using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using Moq;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetDates
{
    public class GetDatesQueryTests
    {
        [Fact]
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown()
        {
            Action action = () => new GetDatesQuery(null, Shared.HttpClient);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");
        }

        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();

            Action action = () => new GetDatesQuery(mockUriGenerator.Object, null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public void ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();

            var getDatesQuery = new GetDatesQuery(mockUriGenerator.Object, Shared.HttpClient);

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