using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using FluentCineworld.Sites;
using NSubstitute;
using Xunit;

namespace FluentCineworld.Tests.Sites
{
    public class SiteDetailsQueryTests
    {
        [Fact]
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown() =>
            FluentActions
                .Invoking(() => new SiteDetailsQuery(null, Shared.HttpClient))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("uriGenerator");

        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown() =>
            FluentActions
                .Invoking(() => new SiteDetailsQuery(Substitute.For<IUriGenerator>(), null))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("httpClient");

        [Fact]
        public async Task ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = Substitute.For<IUriGenerator>();

            var siteDetailsQuery = new SiteDetailsQuery(mockUriGenerator, Shared.HttpClient);

            var thrownException = await FluentActions
                .Awaiting(() => siteDetailsQuery.ExecuteAsync(null, TestContext.Current.CancellationToken))
                .Should()
                .ThrowExactlyAsync<ArgumentNullException>();

            thrownException.Which.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinema_ThenDetailsAreReturned()
        {
            var uriGenerator = new UriGenerator();

            var getFilmsQuery = new SiteDetailsQuery(uriGenerator, Shared.HttpClient);

            var site = await getFilmsQuery.ExecuteAsync(Cinema.LondonLeicesterSquare, TestContext.Current.CancellationToken);

            site.Should().NotBeNull();
        }
    }
}
