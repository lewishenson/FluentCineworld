using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetFilms;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetFilms
{
    public class GetFilmsQueryTests
    {
        [Fact]
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => new GetFilmsQuery(null, Shared.HttpClient);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");
        }

         [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            Action action = () => new GetFilmsQuery(uriGenerator, null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public void ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var uriGenerator = new UriGenerator();

            var getFilmsQuery = new GetFilmsQuery(uriGenerator, Shared.HttpClient);

            Func<Task> action = async () => await getFilmsQuery.ExecuteAsync(null, DateTime.UtcNow);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinemaAndDates_ThenFilmsAreReturned()
        {
            var uriGenerator = new UriGenerator();

            var getFilmsQuery = new GetFilmsQuery(uriGenerator, Shared.HttpClient);

            var films = await getFilmsQuery.ExecuteAsync(Cinema.LondonLeicesterSquare, DateTime.UtcNow);

            films.Should().NotBeNullOrEmpty();
        }
    }
}