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

            actualCollection.ShouldBeEquivalentTo(Enumerable.Empty<SiteDetails>());
        }

        [Fact]
        public void GivenThereAreMultipleSitesInTheData_WhenMapping_ThenMultipleSitesShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data/cinemas.json");
            var siteMapper = new SiteMapper();

            var actualCollection = siteMapper.Map(json);

            actualCollection.Count().ShouldBeEquivalentTo(84);
        }

        [Fact]
        public void GivenThereIsValidData_WhenMapping_ThenAPopulatedSiteDetailsShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data/cinemas.json");
            var siteMapper = new SiteMapper();

            var actual = siteMapper.Map(json).First();

            var expected = new SiteDetails
                {
                    Id = 1010804,
                    Name = "Aberdeen - Queens Links",
                    Address = "Queens Link Leisure Park, Links Road, Aberdeen AB24 5EN",
                    PhoneNumber = "0871 200 2000",
                    Latitude = 57.150299072265625d,
                    Longitude = -2.0779600143432617d,
                    Url = "aberdeen-queens-links"
            };

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}