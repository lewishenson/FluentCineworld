using System.IO;
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
        public void GivenThereIsNoMatchingCinema_ThenNullShouldBeReturned()
        {
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == string.Empty);
            var query = new SiteDetailsQuery(webClient, new SiteMapper());

            var actual = query.Execute(Cinema.MiltonKeynes);

            actual.Should().BeNull();
        }

        [Fact]
        public void GivenThereIsAMatchingCinema_ThenItsSiteShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data\cinemas.json");
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == json);
            var query = new SiteDetailsQuery(webClient, new SiteMapper());

            var actual = query.Execute(Cinema.MiltonKeynes);

            actual.Should().NotBeNull();
        }
    }
}
