using System;
using System.Linq;
using FluentAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class FilterTests
    {
        [Fact]
        public void GivenThereAreDates_WhenNoFilterIsSetup_ThenAllDatesWillBeReturned()
        {
            var date1 = new DateTime(2018, 4, 1);
            var date2 = new DateTime(2018, 4, 2);
            var allDates = new[] { date1, date2 };

            var filter = new Filter();

            var filteredDates = allDates.Where(filter.Apply);

            filteredDates.ShouldAllBeEquivalentTo(allDates);
        }

        [Fact]
        public void GivenThereAreDates_WhenDayOfWeekFilterIsSetup_ThenExpectedDatesWillBeReturned()
        {
            var friday = new DateTime(2014, 1, 3);
            var saturday = new DateTime(2014, 1, 4);
            var sunday = new DateTime(2014, 1, 5);
            var allDates = new[] { friday, saturday, sunday };

            var filter = new Filter();
            filter.DayOfWeek(DayOfWeek.Friday);
            filter.DayOfWeek(DayOfWeek.Saturday);

            var filteredDates = allDates.Where(filter.Apply);

            var expectedDates = new[] { friday, saturday };
            filteredDates.ShouldAllBeEquivalentTo(expectedDates);
        }

        [Fact]
        public void GivenThereAreDates_WhenTheFromFilterIsSetup_ThenExpectedDatesWillBeReturned()
        {
            var today = new DateTime(2014, 1, 1);
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);
            var allDates = new[] { yesterday, today, tomorrow };

            var filter = new Filter();
            filter.From(today);

            var filteredDates = allDates.Where(filter.Apply);

            var expectedDates = new[] { today, tomorrow };
            filteredDates.ShouldAllBeEquivalentTo(expectedDates);
        }

        [Fact]
        public void GivenThereAreDates_WhenTheToFilterIsSetup_ThenExpectedDatesWillBeReturned()
        {
            var today = new DateTime(2014, 1, 1);
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);
            var allDates = new[] { yesterday, today, tomorrow };

            var filter = new Filter();
            filter.To(today);

            var filteredDates = allDates.Where(filter.Apply);

            var expectedDates = new[] { yesterday, today };
            filteredDates.ShouldAllBeEquivalentTo(expectedDates);
        }
    }
}