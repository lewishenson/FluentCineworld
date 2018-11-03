using System;
using FluentAssertions;
using FluentCineworld.Listings.GetFilms;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetFilms
{
    public class UriGeneratorTests
    {
        [Fact]
        public void Listings_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            Action action = () => UriGenerator.Listings(null, DateTime.UtcNow);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        public void Listings_GivenCinemaAndDate_ThenUriReturned()
        {
            var date = new DateTime(2018, 10, 31);

            var uri = UriGenerator.Listings(Cinema.LondonTheO2Greenwich, date);

            uri.Should().Be("https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/film-events/in-cinema/8052/at-date/2018-10-31?attr=&lang=en_GB");
        }
    }
}