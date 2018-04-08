using System.IO;
using System.Linq;
using FluentAssertions;
using FluentCineworld.Sites;
using Xunit;

namespace FluentCineworld.UnitTests.Sites
{
    [Trait("Category", "UnitTest")]
    public class SiteMapperTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenThereIsNoData_WhenMapping_ThenAnEmptyCollectionIsReturned(string json)
        {
            var siteMapper = new SiteMapper();

            var actualCollection = siteMapper.Map(json);

            actualCollection.Should().BeEquivalentTo(Enumerable.Empty<SiteDetails>());
        }

        [Fact]
        public void GivenThereAreMultipleSitesInTheData_WhenMapping_ThenMultipleSitesShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data/cinemas.json");
            var siteMapper = new SiteMapper();

            var actualCollection = siteMapper.Map(json);

            actualCollection.Count().Should().Be(98);
        }

        [Fact]
        public void GivenThereIsValidData_WhenMapping_ThenAPopulatedSiteDetailsShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data/cinemas.json");
            var siteMapper = new SiteMapper();

            var actual = siteMapper.Map(json).First();

            var expected = new SiteDetails
                {
                    Id = 8014,
                    DisplayName = "Aberdeen - Queens Links",
                    Address = "Queens Links Leisure Park, Links Road, AB24 5EN, Aberdeen",
                    Link = "/cinemas/aberdeen-queens-links"
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}