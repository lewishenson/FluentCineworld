using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;
using NSubstitute;
using Xunit;

namespace FluentCineworld.Tests.Listings
{
    public class CinemaListingsTests
    {
        [Fact]
        public void Constructor_GivenNullCinema_ThenArgumentNullExceptionThrown() =>
            FluentActions
                .Invoking(() =>
                    new CinemaListings(
                        null,
                        Substitute.For<IGetDatesQuery>(),
                        Substitute.For<IGetFilmsQuery>(),
                        Substitute.For<IFilter>()
                    )
                )
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("cinema");

        [Fact]
        public void Constructor_GivenNullGetDatesQuery_ThenArgumentNullExceptionThrown() =>
            FluentActions
                .Invoking(() =>
                    new CinemaListings(
                        Cinema.LondonLeicesterSquare,
                        null,
                        Substitute.For<IGetFilmsQuery>(),
                        Substitute.For<IFilter>()
                    )
                )
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("getDatesQuery");

        [Fact]
        public void Constructor_GivenNullGetFilmsQuery_ThenArgumentNullExceptionThrown() =>
            FluentActions
                .Invoking(() =>
                    new CinemaListings(
                        Cinema.LondonLeicesterSquare,
                        Substitute.For<IGetDatesQuery>(),
                        null,
                        Substitute.For<IFilter>()
                    )
                )
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("getFilmsQuery");

        [Fact]
        public void Constructor_GivenNullFilter_ThenArgumentNullExceptionThrown() =>
            FluentActions
                .Invoking(() =>
                    new CinemaListings(
                        Cinema.LondonTheO2Greenwich,
                        Substitute.For<IGetDatesQuery>(),
                        Substitute.For<IGetFilmsQuery>(),
                        null
                    )
                )
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("filter");

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ThenWillReturnFilms()
        {
            var mockGetDatesQuery = Substitute.For<IGetDatesQuery>();

            var date1 = new DateOnly(2018, 10, 1);
            var date2 = new DateOnly(2018, 11, 2);
            var dates = new List<DateOnly> { date1, date2 };

            mockGetDatesQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<DateOnly>>(dates));

            var mockGetFilmsQuery = Substitute.For<IGetFilmsQuery>();

            var date1Film1 = new Film { Id = "film-a", Days = [] };
            var date1Film2 = new Film { Id = "film-b", Days = [] };
            var date1Films = new List<Film> { date1Film1, date1Film2 };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date1, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film1 = new Film { Id = "film-c", Days = [] };
            var date2Film2 = new Film { Id = "film-d", Days = [] };
            var date2Films = new List<Film> { date2Film1, date2Film2 };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date2, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(
                Cinema.LondonLeicesterSquare,
                mockGetDatesQuery,
                mockGetFilmsQuery,
                filter
            );

            var films = await cinemaListings.RetrieveAsync(CancellationToken.None);

            films.Should().BeEquivalentTo([date1Film1, date1Film2, date2Film1, date2Film2]);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ThenWillMergeFilms()
        {
            var mockGetDatesQuery = Substitute.For<IGetDatesQuery>();

            var date1 = new DateOnly(2018, 10, 1);
            var date2 = new DateOnly(2018, 11, 2);
            var dates = new List<DateOnly> { date1, date2 };

            mockGetDatesQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<DateOnly>>(dates));

            var mockGetFilmsQuery = Substitute.For<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date1 } },
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date1, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date2 } },
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date2, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(
                Cinema.LondonLeicesterSquare,
                mockGetDatesQuery,
                mockGetFilmsQuery,
                filter
            );

            var films = await cinemaListings.RetrieveAsync(CancellationToken.None);

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 },
                    new Day { Date = date2 },
                },
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ShouldFilterByFrom()
        {
            var mockGetDatesQuery = Substitute.For<IGetDatesQuery>();

            var date1 = new DateOnly(2018, 10, 1);
            var date2 = new DateOnly(2018, 11, 2);
            var dates = new List<DateOnly> { date1, date2 };

            mockGetDatesQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<DateOnly>>(dates));

            var mockGetFilmsQuery = Substitute.For<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date1 } },
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date1, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date2 } },
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date2, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(
                Cinema.LondonLeicesterSquare,
                mockGetDatesQuery,
                mockGetFilmsQuery,
                filter
            );

            var films = await cinemaListings.From(date2).RetrieveAsync(CancellationToken.None);

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date2 } },
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ShouldFilterByTo()
        {
            var mockGetDatesQuery = Substitute.For<IGetDatesQuery>();

            var date1 = new DateOnly(2018, 10, 1);
            var date2 = new DateOnly(2018, 11, 2);
            var dates = new List<DateOnly> { date1, date2 };

            mockGetDatesQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<DateOnly>>(dates));

            var mockGetFilmsQuery = Substitute.For<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date1 } },
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date1, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date2 } },
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date2, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(
                Cinema.LondonLeicesterSquare,
                mockGetDatesQuery,
                mockGetFilmsQuery,
                filter
            );

            var films = await cinemaListings.To(date1).RetrieveAsync(CancellationToken.None);

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date1 } },
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ShouldFilterByDayOfWeek()
        {
            var mockGetDatesQuery = Substitute.For<IGetDatesQuery>();

            var date1 = new DateOnly(2018, 10, 1);
            var date2 = new DateOnly(2018, 11, 2);
            var dates = new List<DateOnly> { date1, date2 };

            mockGetDatesQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<DateOnly>>(dates));

            var mockGetFilmsQuery = Substitute.For<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date1 } },
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date1, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date2 } },
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery
                .ExecuteAsync(Cinema.LondonLeicesterSquare, date2, CancellationToken.None)
                .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(
                Cinema.LondonLeicesterSquare,
                mockGetDatesQuery,
                mockGetFilmsQuery,
                filter
            );

            var films = await cinemaListings.ForDayOfWeek(date1.DayOfWeek).RetrieveAsync(CancellationToken.None);

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day> { new Day { Date = date1 } },
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }
    }
}
