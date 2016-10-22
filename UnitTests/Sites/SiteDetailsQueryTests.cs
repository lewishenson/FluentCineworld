using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Sites;
using FluentCineworld.Utilities;
using Moq;
using Xunit;

namespace FluentCineworld.UnitTests.Sites
{
    [Trait("Category", "UnitTest")]
    public class SiteDetailsQueryTests
    {
        [Fact]
        public async void GivenThereIsNoMatchingCinema_ThenNullShouldBeReturned()
        {
            var httpClient = Mock.Of<IHttpClient>();
            Mock.Get(httpClient)
                .Setup(w => w.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(""));

            var query = new SiteDetailsQuery(httpClient, new SiteMapper());

            var actual = await query.ExecuteAsync(Cinema.MiltonKeynes);

            actual.Should().BeNull();
        }

        [Fact]
        public async void GivenThereIsAMatchingCinema_ThenItsSiteShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data\cinemas.json");

            var httpClient = Mock.Of<IHttpClient>();
            Mock.Get(httpClient)
                .Setup(w => w.GetContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(json));

            var query = new SiteDetailsQuery(httpClient, new SiteMapper());

            var actual = await query.ExecuteAsync(Cinema.MiltonKeynes);

            actual.Should().NotBeNull();
        }
    }
}
