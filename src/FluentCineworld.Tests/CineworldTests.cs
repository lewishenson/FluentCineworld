using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace FluentCineworld.Tests
{
    public class CineworldTests
    {
        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            Action action = () => new Cineworld(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public void WhatsOn_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var cineworld = new Cineworld(Shared.HttpClient);

            Action action = () => cineworld.WhatsOn(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        public void WhatsOn_GivenCinema_ThenListingsReturned()
        {
            var cineworld = new Cineworld(Shared.HttpClient);

            var listings = cineworld.WhatsOn(Cinema.Northampton);

            listings.Should().NotBeNull();
        }

        [Fact]
        public async Task SiteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var cineworld = new Cineworld(Shared.HttpClient);

            Func<Task> action = async () => await cineworld.SiteAsync(null);

            var thrownException = await action.Should().ThrowExactlyAsync<ArgumentNullException>();
            thrownException.Which.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task SiteAsync_GivenCinema_ThenDetailsReturned()
        {
            var cineworld = new Cineworld(Shared.HttpClient);

            var site = await cineworld.SiteAsync(Cinema.Northampton);

            site.Should().NotBeNull();
        }
    }
}