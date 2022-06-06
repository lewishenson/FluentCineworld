using System;
using System.Linq;
using FluentAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.Tests.Listings
{
    public class FilterTests
    {
        [Fact]
        public void GivenThereAreDates_WhenNoFilterIsSetup_ThenAllDatesWillBeReturned()
        {
            var date1 = new DateOnly(2018, 4, 1);
            var date2 = new DateOnly(2018, 4, 2);
            var allDates = new[] { date1, date2 };

            var filter = new Filter();

            var filteredDates = allDates.Where(filter.Apply);

            filteredDates.Should().BeEquivalentTo(allDates);
        }

        [Fact]
        public void GivenThereAreDates_WhenDayOfWeekFilterIsSetup_ThenExpectedDatesWillBeReturned()
        {
            var friday = new DateOnly(2014, 1, 3);
            var saturday = new DateOnly(2014, 1, 4);
            var sunday = new DateOnly(2014, 1, 5);
            var allDates = new[] { friday, saturday, sunday };

            var filter = new Filter();
            filter.DayOfWeek(DayOfWeek.Friday);
            filter.DayOfWeek(DayOfWeek.Saturday);

            var filteredDates = allDates.Where(filter.Apply);

            var expectedDates = new[] { friday, saturday };
            filteredDates.Should().BeEquivalentTo(expectedDates);
        }

        [Fact]
        public void GivenThereAreDates_WhenTheFromFilterIsSetup_ThenExpectedDatesWillBeReturned()
        {
            var today = new DateOnly(2014, 1, 1);
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);
            var allDates = new[] { yesterday, today, tomorrow };

            var filter = new Filter();
            filter.From(today);

            var filteredDates = allDates.Where(filter.Apply);

            var expectedDates = new[] { today, tomorrow };
            filteredDates.Should().BeEquivalentTo(expectedDates);
        }

        [Fact]
        public void GivenThereAreDates_WhenTheToFilterIsSetup_ThenExpectedDatesWillBeReturned()
        {
            var today = new DateOnly(2014, 1, 1);
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);
            var allDates = new[] { yesterday, today, tomorrow };

            var filter = new Filter();
            filter.To(today);

            var filteredDates = allDates.Where(filter.Apply);

            var expectedDates = new[] { yesterday, today };
            filteredDates.Should().BeEquivalentTo(expectedDates);
        }
    }
}
