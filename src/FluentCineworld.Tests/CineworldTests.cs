using System;
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
    }
}