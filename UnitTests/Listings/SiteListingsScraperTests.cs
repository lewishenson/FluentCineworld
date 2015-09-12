using FluentAssertions;
using FluentCineworld.OldListings;
using FluentCineworld.Utilities;
using Moq;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class SiteListingsScraperTests
    {
        [Fact]
        public void GivenTheCineworldSiteIsUnavailable_WhenTheFilmsAreRequested_ThenAnEmptyCollectionWillBeReturned()
        {
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == string.Empty);
            var scraper = new SiteListingsScraper(webClient);

            var films = scraper.Scrape(Cinema.MiltonKeynes);

            films.Should().BeEmpty();
        }
    }
}