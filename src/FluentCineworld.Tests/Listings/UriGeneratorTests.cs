using System;
using FluentAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.Tests.Listings
{
    public class UriGeneratorTests
    {
        [Fact]
        public void ForDatesWithListings_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => uriGenerator.ForDatesWithListings(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        public void ForDatesWithListings_GivenCinema_ThenUriReturned()
        {
            try
            {
                SystemDate.UtcNow = () => new DateTime(2018, 10, 31);

                var uriGenerator = new UriGenerator();

                var uri = uriGenerator.ForDatesWithListings(Cinema.LondonTheO2Greenwich);

                uri.Should().Be("https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/dates/in-cinema/8052/until/2019-10-31?attr=&lang=en_GB");
            }
            finally
            {
                SystemDate.UtcNow = () => DateTime.UtcNow;
            }
        }

        [Fact]
        public void ForListings_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => uriGenerator.ForListings(null, DateTime.UtcNow);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        public void ForListings_GivenCinemaAndDate_ThenUriReturned()
        {
            var date = new DateTime(2018, 10, 31);

            var uriGenerator = new UriGenerator();

            var uri = uriGenerator.ForListings(Cinema.LondonTheO2Greenwich, date);

            uri.Should().Be("https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/film-events/in-cinema/8052/at-date/2018-10-31?attr=&lang=en_GB");
        }
    }
}