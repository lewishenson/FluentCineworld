using System;
using FluentAssertions;
using FluentCineworld.Sites;
using Xunit;

namespace FluentCineworld.Tests.Sites
{
    [Collection("Time-sensitive tests")]
    public class UriGeneratorTests
    {
        [Fact]
        public void ForCinemaSites_ThenUriReturned()
        {
            try
            {
                SystemDate.UtcNow = () => new DateTime(2018, 10, 31);

                var uriGenerator = new UriGenerator();

                var uri = uriGenerator.ForCinemaSites();

                uri.Should()
                    .Be(
                        "https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/cinemas/with-event/until/2019-10-31?attr=&lang=en_GB"
                    );
            }
            finally
            {
                SystemDate.UtcNow = () => DateTime.UtcNow;
            }
        }
    }
}
