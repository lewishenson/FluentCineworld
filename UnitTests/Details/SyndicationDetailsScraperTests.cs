using FluentAssertions;

using FluentCineworld.Details;
using FluentCineworld.Utilities;

using Moq;

using System.IO;

using Xunit;

namespace FluentCineworld.UnitTests.Details
{
    [Trait("Category", "UnitTest")]
    public class SyndicationDetailsScraperTests
    {
        [Fact]
        public void GivenTheCineworldSiteIsUnavailable_WhenTheDetailsAreRequested_ThenNullWillBeReturned()
        {
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == string.Empty);
            var scraper = new SyndicationDetailsScraper(webClient);

            var details = scraper.Scrape(Cinema.Wembley);

            details.Should().BeNull();
        }

        [Fact]
        public void GivenTheCineworldSiteIsAvailable_WhenTheDetailsAreRequest_ThenAnInstanceIsReturned()
        {
            var content = File.ReadAllText(@"Data\cinema_names.xml");
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == content);
            var scraper = new SyndicationDetailsScraper(webClient);

            var details = scraper.Scrape(Cinema.Cambridge);

            details.Id.Should().Be(7);
            details.Name.Should().Be("Cineworld Cambridge");
            details.Phone.Should().Be("0871 200 2000");
            details.Address.Should().Be("Cambridge Leisure Park, Clifton Way, Cambridge");
            details.PostCode.Should().Be("CB1 7DY");
            details.Url.Should().Be("http://cineworld.co.uk/cinemas/7/information");
        }
    }
}