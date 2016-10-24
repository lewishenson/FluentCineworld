using System;
using System.Linq;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.UnitTests.Builders;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class FilterTests
    {
        [Fact]
        public void GivenThereAreFilms_WhenNoFilterIsSetup_ThenAllFilmsWillBeReturned()
        {
            var film1 = BuildA.Film()
                              .WithDay(day => day.Build())
                              .Build();

            var film2 = BuildA.Film()
                              .WithDay(day => day.Build())
                              .Build();

            var allFilms = new[] { film1, film2 };

            var filter = new Filter();

            var filteredFilms = allFilms.Where(filter.Apply);

            filteredFilms.ShouldAllBeEquivalentTo(allFilms);
        }

        [Fact]
        public void GivenThereAreFilms_WhenDayOfWeekFilterIsSetup_ThenExpectedFilmsWillBeReturned()
        {
            var fridayFilm = BuildA.Film()
                                   .WithDay(day => day.WithDate(new DateTime(2014, 1, 3))
                                                      .Build())
                                   .Build();

            var saturdayFilm = BuildA.Film()
                                     .WithDay(day => day.WithDate(new DateTime(2014, 1, 4))
                                                        .Build())
                                     .Build();

            var sundayFilm = BuildA.Film()
                                   .WithDay(day => day.WithDate(new DateTime(2014, 1, 5))
                                                      .Build())
                                   .Build();

            var allFilms = new[] { fridayFilm, saturdayFilm, sundayFilm };

            var filter = new Filter();
            filter.DayOfWeek(DayOfWeek.Friday);
            filter.DayOfWeek(DayOfWeek.Saturday);

            var filteredFilms = allFilms.Where(filter.Apply);

            var expectedFilms = new[] { fridayFilm, saturdayFilm };
            filteredFilms.ShouldAllBeEquivalentTo(expectedFilms);
        }

        [Fact]
        public void GivenThereAreFilms_WhenTheFromFilterIsSetup_ThenExpectedFilmsWillBeReturned()
        {
            var today = new DateTime(2014, 1, 1);

            var yesterdayFilm = BuildA.Film()
                                      .WithDay(day => day.WithDate(today.AddDays(-1))
                                                         .Build())
                                      .Build();

            var todayFilm = BuildA.Film()
                                  .WithDay(day => day.WithDate(today)
                                                     .Build())
                                  .Build();

            var tomorrowFilm = BuildA.Film()
                                     .WithDay(day => day.WithDate(today.AddDays(1))
                                                        .Build())
                                     .Build();

            var allFilms = new[] { yesterdayFilm, todayFilm, tomorrowFilm };

            var filter = new Filter();
            filter.From(today);

            var filteredFilms = allFilms.Where(filter.Apply);

            var expectedFilms = new[] { todayFilm, tomorrowFilm };
            filteredFilms.ShouldAllBeEquivalentTo(expectedFilms);
        }

        [Fact]
        public void GivenThereAreFilms_WhenTheToFilterIsSetup_ThenExpectedFilmsWillBeReturned()
        {
            var today = new DateTime(2014, 1, 1);

            var yesterdayFilm = BuildA.Film()
                                      .WithDay(day => day.WithDate(today.AddDays(-1))
                                                         .Build())
                                      .Build();

            var todayFilm = BuildA.Film()
                                  .WithDay(day => day.WithDate(today)
                                                     .Build())
                                  .Build();

            var tomorrowFilm = BuildA.Film()
                                     .WithDay(day => day.WithDate(today.AddDays(1))
                                                        .Build())
                                     .Build();

            var allFilms = new[] { yesterdayFilm, todayFilm, tomorrowFilm };

            var filter = new Filter();
            filter.To(today);

            var filteredFilms = allFilms.Where(filter.Apply);

            var expectedFilms = new[] { yesterdayFilm, todayFilm };
            filteredFilms.ShouldAllBeEquivalentTo(expectedFilms);
        }
    }
}