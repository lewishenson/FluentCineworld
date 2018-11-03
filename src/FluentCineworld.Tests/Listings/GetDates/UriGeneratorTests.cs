using System;
using FluentAssertions;
using FluentCineworld.Listings.GetDates;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetDates
{
    public class UriGeneratorTests
    {
        [Fact]
        public void DatesWithListings_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            Action action = () => UriGenerator.DatesWithListings(null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        public void DatesWithListings_GivenCinema_ThenUriReturned()
        {
            try
            {
                SystemDate.UtcNow = () => new DateTime(2018, 10, 31);

                var uri = UriGenerator.DatesWithListings(Cinema.LondonTheO2Greenwich);

                uri.Should().Be("https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/dates/in-cinema/8052/until/2019-10-31?attr=&lang=en_GB");
            }
            finally
            {
                SystemDate.UtcNow = () => DateTime.UtcNow;
            }
        }
    }
}