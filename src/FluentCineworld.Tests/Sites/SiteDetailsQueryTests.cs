using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Sites;
using Moq;
using Xunit;

namespace FluentCineworld.Tests.Sites
{
    public class SiteDetailsQueryTests
    {
        [Fact]
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown()
        {
            Action action = () => new SiteDetailsQuery(null, Shared.HttpClient);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");
        }

        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();

            Action action = () => new SiteDetailsQuery(mockUriGenerator.Object, null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public async Task ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();

            var siteDetailsQuery = new SiteDetailsQuery(mockUriGenerator.Object, Shared.HttpClient);

            Func<Task> action = async () => await siteDetailsQuery.ExecuteAsync(null);

            var thrownException = await action.Should().ThrowExactlyAsync<ArgumentNullException>();
            thrownException.Which.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinema_ThenDetailsAreReturned()
        {
            var uriGenerator = new UriGenerator();

            var getFilmsQuery = new SiteDetailsQuery(uriGenerator, Shared.HttpClient);

            var site = await getFilmsQuery.ExecuteAsync(Cinema.LondonLeicesterSquare);

            site.Should().NotBeNull();
        }
    }
}