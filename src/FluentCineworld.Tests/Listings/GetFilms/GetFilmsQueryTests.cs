using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetFilms;
using Moq;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetFilms
{
    public class GetFilmsQueryTests
    {
        [Fact]
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown()
        {
            var mockFilmNameFormatter = new Mock<IFilmNameFormatter>();

            Action action = () => new GetFilmsQuery(null, Shared.HttpClient, mockFilmNameFormatter.Object);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");
        }

        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();
            var mockFilmNameFormatter = new Mock<IFilmNameFormatter>();

            Action action = () => new GetFilmsQuery(mockUriGenerator.Object, null, mockFilmNameFormatter.Object);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");
        }

        [Fact]
        public void Constructor_GivenNullFilmNameFormatter_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();

            Action action = () => new GetFilmsQuery(mockUriGenerator.Object, Shared.HttpClient, null);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("filmNameFormatter");
        }

        [Fact]
        public void ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();
            var mockFilmNameFormatter = new Mock<IFilmNameFormatter>();

            var getFilmsQuery = new GetFilmsQuery(mockUriGenerator.Object, Shared.HttpClient, mockFilmNameFormatter.Object);

            Func<Task> action = async () => await getFilmsQuery.ExecuteAsync(null, DateTime.UtcNow);

            action.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinemaAndDates_ThenFilmsAreReturned()
        {
            var uriGenerator = new UriGenerator();
            var filmNameFormatter = new FilmNameFormatter();

            var getFilmsQuery = new GetFilmsQuery(uriGenerator, Shared.HttpClient, filmNameFormatter);

            var films = await getFilmsQuery.ExecuteAsync(Cinema.LondonLeicesterSquare, DateTime.UtcNow.AddDays(1));

            films.Should().NotBeNullOrEmpty();
        }
    }
}