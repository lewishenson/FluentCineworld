using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetDates
{
    public class GetDatesQueryTests
    {
        [Fact]
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => new GetDatesQuery(null, Shared.HttpClient);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");
        }

         [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => new GetDatesQuery(uriGenerator, null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public void ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            var getDatesQuery = new GetDatesQuery(uriGenerator, Shared.HttpClient);

            Func<Task> action = async () => await getDatesQuery.ExecuteAsync(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinema_ThenDatesAreReturned()
        {
            var uriGenerator = new UriGenerator();

            var getDatesQuery = new GetDatesQuery(uriGenerator, Shared.HttpClient);

            var dates = await getDatesQuery.ExecuteAsync(Cinema.MiltonKeynes);

            dates.Should().NotBeNullOrEmpty();
        }
    }
}