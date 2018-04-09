using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetFilms;
using FluentCineworld.Utilities;
using Moq;
using Xunit;

namespace FluentCineworld.UnitTests.Listings.GetFilms
{
    [Trait("Category", "UnitTest")]
    public class GetFilmsQueryTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GivenThereIsNoData_WhenExecuted_ThenAnEmptyCollectionIsReturned(string json)
        {
            var httpClient = Mock.Of<IHttpClient>();

            Mock.Get(httpClient)
                .Setup(client => client.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new GetFilmsQuery(httpClient);

            var films = await query.ExecuteAsync(Cinema.MiltonKeynes, DateTime.UtcNow);

            films.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenThereIsData_WhenExecuted_ThenTheCorrectNumberOfFilmsAreReturned()
        {
            var httpClient = Mock.Of<IHttpClient>();

            var json = File.ReadAllText(@"Data/listings.json");

            Mock.Get(httpClient)
                .Setup(client => client.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new GetFilmsQuery(httpClient);

            var films = await query.ExecuteAsync(Cinema.MiltonKeynes, DateTime.UtcNow);

            films.Count().Should().Be(14);
        }

        [Fact]
        public async Task GivenThereIsData_WhenExecuted_ThenTheFilmPropertiesArePopulated()
        {
            var httpClient = Mock.Of<IHttpClient>();

            var json = File.ReadAllText(@"Data/listings.json");

            Mock.Get(httpClient)
                .Setup(client => client.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new GetFilmsQuery(httpClient);

            var targetDate = new DateTime(2018, 4, 5);
            var films = await query.ExecuteAsync(Cinema.MiltonKeynes, targetDate);

            var actualBlackPantherFilm = films.SingleOrDefault(film => film.Name == "Black Panther");
            actualBlackPantherFilm.Should().NotBeNull();

            var expectedBlackPantherFilm = new Film
            {
                Id = "ho00004790",
                Name = "Black Panther",
                Rating = "12A",
                Duration = 134,
                Days = new[]
                {
                    new Day
                    {
                        Date = targetDate,
                        Showings = new[]
                        {
                            new Showing
                            {
                               AttributeIds = new[] {"12a", "2d", "audio-described"},
                               AttributeTexts = new[] {"12A", "2D"},
                               Time = new DateTime(2018, 4, 5, 17, 0, 0)
                            },
                            new Showing
                            {
                               AttributeIds = new[] {"12a", "2d", "audio-described"},
                               AttributeTexts = new[] {"12A", "2D"},
                               Time = new DateTime(2018, 4, 5, 20, 0, 0)
                            }
                        }
                    }
                }
            };

            actualBlackPantherFilm.Should().BeEquivalentTo(expectedBlackPantherFilm);
        }

        [Fact]
        public async Task GivenThereIsData_WhenExecuted_ThenTheFilmNameIsFormatted()
        {
            var httpClient = Mock.Of<IHttpClient>();

            var json = File.ReadAllText(@"Data/listings.json");

            Mock.Get(httpClient)
                .Setup(client => client.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new GetFilmsQuery(httpClient);

            var targetDate = new DateTime(2018, 4, 5);
            var films = await query.ExecuteAsync(Cinema.MiltonKeynes, targetDate);

            var actualIsleOfDogsFilm = films.SingleOrDefault(film => film.Id == "ho00005040");
            actualIsleOfDogsFilm.Should().NotBeNull();

            actualIsleOfDogsFilm.Name.Should().Be("Isle Of Dogs");
        }
    }
}