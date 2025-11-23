using System;
using AwesomeAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.Tests.Listings
{
    [Collection("Time-sensitive tests")]
    public class UriGeneratorTests
    {
        [Fact]
        public void ForDatesWithListings_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => uriGenerator.ForDatesWithListings(null);

            action
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("cinema");
        }

        [Fact]
        public void ForDatesWithListings_GivenCinema_ThenUriReturned()
        {
            try
            {
                SystemDate.UtcNow = () => new DateTime(2019, 10, 31);

                var uriGenerator = new UriGenerator();

                var uri = uriGenerator.ForDatesWithListings(Cinema.LondonTheO2Greenwich);

                uri.Should()
                    .Be(
                        "https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/dates/in-cinema/077/until/2020-10-31?attr=&lang=en_GB"
                    );
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

            Action action = () =>
                uriGenerator.ForListings(null, DateOnly.FromDateTime(DateTime.UtcNow));

            action
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should()
                .Be("cinema");
        }

        [Fact]
        public void ForListings_GivenCinemaAndDate_ThenUriReturned()
        {
            var date = new DateOnly(2018, 10, 31);

            var uriGenerator = new UriGenerator();

            var uri = uriGenerator.ForListings(Cinema.LondonTheO2Greenwich, date);

            uri.Should()
                .Be(
                    "https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/film-events/in-cinema/077/at-date/2018-10-31?attr=&lang=en_GB"
                );
        }
    }
}
