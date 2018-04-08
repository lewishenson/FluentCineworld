using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;
using FluentCineworld.UnitTests.Builders;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class CineworldListingsTests
    {
        private static readonly Cinema Cinema = Cinema.Bedford;

        [Fact]
        public async Task GivenThereIsSingleDateWithMultipleFilmsAndNoFilter_WhenRetrieved_ThenTheListingsAreReturned()
        {           
            var date = new DateTime(2018, 4, 6);

            var getDatesQuery = Mock.Of<IGetDatesQuery>();
            this.SetupGetDatesQueryMock(getDatesQuery, date);

            var filter = Mock.Of<IFilter>();

            Mock.Get(filter)
                .Setup(f => f.Apply(It.IsAny<DateTime>()))
                .Returns(true);

            var film1 = BuildA.Film().Build();
            var film2 = BuildA.Film().Build();

            var getFilmsQuery = Mock.Of<IGetFilmsQuery>();
            this.SetupGetFilmsQueryMock(getFilmsQuery, date, film1, film2);

            var cineworldListings = new CineworldListings(Cinema, getDatesQuery, filter, getFilmsQuery);

            var actualFilms = await cineworldListings.RetrieveAsync();
            
            actualFilms.Should().BeEquivalentTo(new[] { film1, film2 });
        }

        [Fact]
        public async Task GivenThereAreMultipleDatesWithMultipleFilmsAndNoFilter_WhenRetrieved_ThenTheListingsAreReturned()
        {
            var date1 = new DateTime(2018, 4, 6);
            var date2 = new DateTime(2018, 4, 7);

            var getDatesQuery = Mock.Of<IGetDatesQuery>();
            this.SetupGetDatesQueryMock(getDatesQuery, date1, date2);

            var filter = Mock.Of<IFilter>();

            Mock.Get(filter)
                .Setup(f => f.Apply(It.IsAny<DateTime>()))
                .Returns(true);

            var film1 = BuildA.Film().Build();
            var film2 = BuildA.Film().Build();

            var getFilmsQuery = Mock.Of<IGetFilmsQuery>();
            this.SetupGetFilmsQueryMock(getFilmsQuery, date1, film1);
            this.SetupGetFilmsQueryMock(getFilmsQuery, date2, film2);

            var cineworldListings = new CineworldListings(Cinema, getDatesQuery, filter, getFilmsQuery);

            var actualFilms = await cineworldListings.RetrieveAsync();
            
            actualFilms.Should().BeEquivalentTo(new[] { film1, film2 });
        }

        [Fact]
        public async Task GivenThereAreMultipleDatesWithSingleFilmsAndNoFilter_WhenRetrieved_ThenTheMergedListingsAreReturned()
        {
            var date1 = new DateTime(2018, 4, 6);
            var date2 = new DateTime(2018, 4, 7);

            var getDatesQuery = Mock.Of<IGetDatesQuery>();
            this.SetupGetDatesQueryMock(getDatesQuery, date1, date2);

            var filter = Mock.Of<IFilter>();

            Mock.Get(filter)
                .Setup(f => f.Apply(It.IsAny<DateTime>()))
                .Returns(true);
            
            var filmOnDate1 = BuildA.Film()
                                    .WithId("A")
                                    .WithDay(day => day.WithDate(date1).Build())
                                    .Build();

            var filmOnDate2 = BuildA.Film()
                                    .WithId("A")
                                    .WithDay(day => day.WithDate(date2).Build())
                                    .Build();

            var getFilmsQuery = Mock.Of<IGetFilmsQuery>();
            this.SetupGetFilmsQueryMock(getFilmsQuery, date1, filmOnDate1);
            this.SetupGetFilmsQueryMock(getFilmsQuery, date2, filmOnDate2);

            var cineworldListings = new CineworldListings(Cinema, getDatesQuery, filter, getFilmsQuery);

            var actualFilms = await cineworldListings.RetrieveAsync();

            var expectedFilm = BuildA.Film()
                                     .WithId("A")
                                     .WithDay(day => day.WithDate(date1).Build())
                                     .WithDay(day => day.WithDate(date2).Build())
                                     .Build();

            actualFilms.Should().BeEquivalentTo(new[] { expectedFilm });
        }

        [Fact]
        public async Task GivenTheFilterHasBeenConfigured_WhenRetrieved_ThenTheFilterIsUsed()
        {
            var getDatesQuery = Mock.Of<IGetDatesQuery>();
            this.SetupGetDatesQueryMock(getDatesQuery);

            var filter = Mock.Of<IFilter>();

            Mock.Get(filter)
                .Setup(f => f.Apply(It.IsAny<DateTime>()))
                .Returns(true);
            
            var getFilmsQuery = Mock.Of<IGetFilmsQuery>();

            var cineworldListings = new CineworldListings(Cinema, getDatesQuery, filter, getFilmsQuery);

            var fromDate = new DateTime(2018, 4, 1);
            cineworldListings.From(fromDate);

            var toDate = new DateTime(2018, 4, 30);
            cineworldListings.To(toDate);

            cineworldListings.ForDayOfWeek(DayOfWeek.Friday);

            await cineworldListings.RetrieveAsync();

            Mock.Get(filter)
                .Verify(f => f.From(fromDate));

            Mock.Get(filter)
                .Verify(f => f.To(toDate));

            Mock.Get(filter)
                .Verify(f => f.DayOfWeek(DayOfWeek.Friday));
        }

        [Fact]
        public async Task GivenThereAreMultipleFilms_WhenRetrieved_ThenTheFilmsAreOrdered()
        {
            var date = new DateTime(2018, 4, 6);

            var getDatesQuery = Mock.Of<IGetDatesQuery>();
            this.SetupGetDatesQueryMock(getDatesQuery, date);

            var filter = Mock.Of<IFilter>();

            Mock.Get(filter)
                .Setup(f => f.Apply(It.IsAny<DateTime>()))
                .Returns(true);

            var film1 = BuildA.Film().WithName("A").Build();
            var film2 = BuildA.Film().WithName("B").Build();
            var film3 = BuildA.Film().WithName("C").Build();

            var getFilmsQuery = Mock.Of<IGetFilmsQuery>();
            this.SetupGetFilmsQueryMock(getFilmsQuery, date, film3, film1, film2);

            var cineworldListings = new CineworldListings(Cinema, getDatesQuery, filter, getFilmsQuery);

            var actualFilms = await cineworldListings.RetrieveAsync();

            actualFilms.Should().BeEquivalentTo(new[] { film1, film2, film3 }, options => options.WithStrictOrderingFor(film => film));
        }
        
        private void SetupGetDatesQueryMock(IGetDatesQuery getDatesQuery, params DateTime[] dates)
        {
            Mock.Get(getDatesQuery)
                .Setup(query => query.ExecuteAsync(Cinema))
                .Returns(Task.FromResult<IEnumerable<DateTime>>(dates));
        }

        private void SetupGetFilmsQueryMock(IGetFilmsQuery getFilmsQuery, DateTime date, params Film[] films)
        {
            Mock.Get(getFilmsQuery)
                .Setup(query => query.ExecuteAsync(Cinema, date))
                .Returns(Task.FromResult<IEnumerable<Film>>(films));
        }
    }
}