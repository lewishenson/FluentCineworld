using FluentAssertions;
using FluentCineworld.UnitTests.Builders;
using System;
using FluentCineworld.OldListings;
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

            var target = new Filter();

            var filteredFilms = target.Apply(allFilms);

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

            var target = new Filter();
            target.DayOfWeek(DayOfWeek.Friday);
            target.DayOfWeek(DayOfWeek.Saturday);

            var filteredFilms = target.Apply(allFilms);

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

            var target = new Filter();
            target.From(today);

            var filteredFilms = target.Apply(allFilms);

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

            var target = new Filter();
            target.To(today);

            var filteredFilms = target.Apply(allFilms);

            var expectedFilms = new[] { yesterdayFilm, todayFilm };
            filteredFilms.ShouldAllBeEquivalentTo(expectedFilms);
        }
    }
}