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
        public void Constructor_GivenNullUriGenerator_ThenArgumentNullExceptionThrown() =>
            FluentActions.Invoking(() => new GetFilmsQuery(null, Shared.HttpClient, Mock.Of<IFilmNameFormatter>()))
                .Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("uriGenerator");

        [Fact]
        public void Constructor_GivenNullHttpClient_ThenArgumentNullExceptionThrown() =>
            FluentActions.Invoking(() => new GetFilmsQuery(Mock.Of<IUriGenerator>(), null, Mock.Of<IFilmNameFormatter>()))
                .Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("httpClient");

        [Fact]
        public void Constructor_GivenNullFilmNameFormatter_ThenArgumentNullExceptionThrown() =>
            FluentActions.Invoking(() => new GetFilmsQuery(Mock.Of<IUriGenerator>(), Shared.HttpClient, null))
                .Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be("filmNameFormatter");

        [Fact]
        public async Task ExecuteAsync_GivenNullCinema_ThenArgumentNullExceptionThrown()
        {
            var mockUriGenerator = new Mock<IUriGenerator>();
            var mockFilmNameFormatter = new Mock<IFilmNameFormatter>();

            var getFilmsQuery = new GetFilmsQuery(mockUriGenerator.Object, Shared.HttpClient, mockFilmNameFormatter.Object);

            var thrownException = await FluentActions.Awaiting(() => getFilmsQuery.ExecuteAsync(null, DateOnly.FromDateTime(DateTime.UtcNow), default))
                .Should().ThrowExactlyAsync<ArgumentNullException>();

            thrownException.Which.ParamName.Should().Be("cinema");
        }

        [Fact]
        [Trait("Integration", "Http")]
        public async Task ExecuteAsync_GivenCinemaAndDates_ThenFilmsAreReturned()
        {
            var uriGenerator = new UriGenerator();
            var filmNameFormatter = new FilmNameFormatter();

            var getFilmsQuery = new GetFilmsQuery(uriGenerator, Shared.HttpClient, filmNameFormatter);

            var films = await getFilmsQuery.ExecuteAsync(Cinema.LondonLeicesterSquare, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)), default);

            films.Should().NotBeNullOrEmpty();
        }
    }
}