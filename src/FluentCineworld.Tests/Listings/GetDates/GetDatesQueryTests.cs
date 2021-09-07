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
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown() =>
            FluentActions.Invoking(() => new GetDatesQuery(null, Shared.HttpClient))
                .Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");

        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown() =>
            FluentActions.Invoking(() => new GetDatesQuery(Mock.Of<IUriGenerator>(), null))
                .Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");

        [Fact]
        public async Task ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();

            var getDatesQuery = new GetDatesQuery(mockUriGenerator.Object, Shared.HttpClient);

            var thrownException = await FluentActions.Awaiting(() => getDatesQuery.ExecuteAsync(null))
                .Should().ThrowExactlyAsync<ArgumentNullException>();

            thrownException.Which.ParamName.Should().Be("cinema");
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