using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;
using Moq;
using Xunit;

namespace FluentCineworld.Tests.Listings
{
    public class CinemaListingsTests
    {
        [Fact]
        public void Constructor_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();
            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();
            var mockFilter = new Mock<IFilter>();

            Action action = () => new CinemaListings(null, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, mockFilter.Object);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        public void Constructor_GivenNullGetDatesQuery_ThenArgumentNullExceptionThrown()
        {
            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();
            var mockFilter = new Mock<IFilter>();

            Action action = () => new CinemaListings(Cinema.LondonLeicesterSquare, null, mockGetFilmsQuery.Object, mockFilter.Object);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("getDatesQuery");
        }

        [Fact]
        public void Constructor_GivenNullGetFilmsQuery_ThenArgumentNullExceptionThrown()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();
            var mockFilter = new Mock<IFilter>();

            Action action = () => new CinemaListings(Cinema.LondonLeicesterSquare, mockGetDatesQuery.Object, null, mockFilter.Object);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("getFilmsQuery");
        }

        [Fact]
        public void Constructor_GivenNullFilter_ThenArgumentNullExceptionThrown()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();
            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();

            Action action = () => new CinemaListings(Cinema.LondonTheO2Greenwich, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("filter");
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ThenWillReturnFilms()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();

            var date1 = new DateTime(2018, 10, 1);
            var date2 = new DateTime(2018, 11, 2);
            var dates = new List<DateTime> { date1, date2 };

            mockGetDatesQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare))
                             .Returns(Task.FromResult<IEnumerable<DateTime>>(dates));

            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();

            var date1Film1 = new Film { Id = "film-a", Days = Enumerable.Empty<Day>() };
            var date1Film2 = new Film { Id = "film-b", Days = Enumerable.Empty<Day>() };
            var date1Films = new List<Film> { date1Film1, date1Film2 };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date1))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film1 = new Film { Id = "film-c", Days = Enumerable.Empty<Day>() };
            var date2Film2 = new Film { Id = "film-d", Days = Enumerable.Empty<Day>() };
            var date2Films = new List<Film> { date2Film1, date2Film2 };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date2))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(Cinema.LondonLeicesterSquare, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, filter);

            var films = await cinemaListings.RetrieveAsync();

            films.Should().BeEquivalentTo(date1Film1, date1Film2, date2Film1, date2Film2);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ThenWillMergeFilms()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();

            var date1 = new DateTime(2018, 10, 1);
            var date2 = new DateTime(2018, 11, 2);
            var dates = new List<DateTime> { date1, date2 };

            mockGetDatesQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare))
                             .Returns(Task.FromResult<IEnumerable<DateTime>>(dates));

            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 }
                }
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date1))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date2 }
                }
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date2))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(Cinema.LondonLeicesterSquare, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, filter);

            var films = await cinemaListings.RetrieveAsync();

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 },
                    new Day { Date = date2 }
                }
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ShouldFilterByFrom()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();

            var date1 = new DateTime(2018, 10, 1);
            var date2 = new DateTime(2018, 11, 2);
            var dates = new List<DateTime> { date1, date2 };

            mockGetDatesQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare))
                             .Returns(Task.FromResult<IEnumerable<DateTime>>(dates));

            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 }
                }
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date1))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date2 }
                }
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date2))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(Cinema.LondonLeicesterSquare, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, filter);

            var films = await cinemaListings.From(date2).RetrieveAsync();

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date2 }
                }
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ShouldFilterByTo()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();

            var date1 = new DateTime(2018, 10, 1);
            var date2 = new DateTime(2018, 11, 2);
            var dates = new List<DateTime> { date1, date2 };

            mockGetDatesQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare))
                             .Returns(Task.FromResult<IEnumerable<DateTime>>(dates));

            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 }
                }
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date1))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date2 }
                }
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date2))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(Cinema.LondonLeicesterSquare, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, filter);

            var films = await cinemaListings.To(date1).RetrieveAsync();

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 }
                }
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }

        [Fact]
        public async Task RetrieveAsync_GivenValidConstructorArguments_ShouldFilterByDayOfWeek()
        {
            var mockGetDatesQuery = new Mock<IGetDatesQuery>();

            var date1 = new DateTime(2018, 10, 1);
            var date2 = new DateTime(2018, 11, 2);
            var dates = new List<DateTime> { date1, date2 };

            mockGetDatesQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare))
                             .Returns(Task.FromResult<IEnumerable<DateTime>>(dates));

            var mockGetFilmsQuery = new Mock<IGetFilmsQuery>();

            var date1Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 }
                }
            };
            var date1Films = new List<Film> { date1Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date1))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date1Films));

            var date2Film = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date2 }
                }
            };
            var date2Films = new List<Film> { date2Film };

            mockGetFilmsQuery.Setup(query => query.ExecuteAsync(Cinema.LondonLeicesterSquare, date2))
                             .Returns(Task.FromResult<IEnumerable<Film>>(date2Films));

            var filter = new Filter();

            var cinemaListings = new CinemaListings(Cinema.LondonLeicesterSquare, mockGetDatesQuery.Object, mockGetFilmsQuery.Object, filter);

            var films = await cinemaListings.ForDayOfWeek(date1.DayOfWeek).RetrieveAsync();

            films.Count().Should().Be(1);

            var expectedFilm = new Film
            {
                Id = "the-film",
                Days = new List<Day>
                {
                    new Day { Date = date1 }
                }
            };
            films.Single().Should().BeEquivalentTo(expectedFilm);
        }
    }
}