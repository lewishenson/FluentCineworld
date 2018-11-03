using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings.GetDates;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetDates
{
    public class GetDatesQueryTests
    {
        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            Action action = () => new GetDatesQuery(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public void ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var getDatesQuery = new GetDatesQuery(Shared.HttpClient);

            Func<Task> action = async () => await getDatesQuery.ExecuteAsync(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinema_ThenDatesAreReturned()
        {
            var getDatesQuery = new GetDatesQuery(Shared.HttpClient);

            var dates = await getDatesQuery.ExecuteAsync(Cinema.MiltonKeynes);

            dates.Should().NotBeNullOrEmpty();
        }
    }
}