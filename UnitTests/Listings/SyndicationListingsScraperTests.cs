using FluentAssertions;
using FluentCineworld.Listings;
using FluentCineworld.Utilities;
using Moq;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class SyndicationListingsScraperTests
    {
        [Fact]
        public void GivenTheCineworldSiteIsUnavailable_WhenTheFilmsAreRequested_ThenAnEmptyCollectionWillBeReturned()
        {
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == string.Empty);
            var scraper = new SyndicationListingsScraper(webClient);

            var films = scraper.Scrape(Cinema.MiltonKeynes);

            films.Should().BeEmpty();
        }

        [Fact]
        public void GivenTheCineworldSiteIsAvailable_WhenTheFilmsAreRequestedForACinemaWithNoFilms_ThenAnEmptyCollectionWillBeReturned()
        {
            var content = File.ReadAllText(@"Data\listings.xml");
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == content);
            var scraper = new SyndicationListingsScraper(webClient);

            var films = scraper.Scrape(Cinema.Northampton);

            films.Should().BeEmpty();
        }

        [Fact]
        public void GivenTheCineworldSiteIsAvailable_WhenTheFilmsAreRequestedForACinemaWithFilms_ThenAValidCollectionWillBeReturned()
        {
            var content = File.ReadAllText(@"Data\listings.xml");
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == content);
            var scraper = new SyndicationListingsScraper(webClient);

            var films = scraper.Scrape(Cinema.MiltonKeynes);

            films.Should().HaveCount(41);

            var nonStopFilm = films.Last(f => f.Title == "Mission: Impossible - Rogue Nation");
            nonStopFilm.Rating.Should().Be("12A");
            nonStopFilm.Days.Should().HaveCount(8);

            var firstDay = nonStopFilm.Days.First();
            firstDay.Date.Should().Be(new DateTime(2015, 7, 30));
            firstDay.Shows.Should().HaveCount(16);

            var firstShow = firstDay.Shows.First();
            firstShow.ToString().Should().Be("10:45 (2D, Superscreen)");
        }

        [Fact]
        public void GivenAFilmIsInTheListingsTwice_WhenTheFilmsAreRequested_ThenItAppearsOnlyOnce()
        {
            var content = File.ReadAllText(@"Data\listings.xml");
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == content);
            var scraper = new SyndicationListingsScraper(webClient);

            var films = scraper.Scrape(Cinema.MiltonKeynes).ToList();

            var insideOutFilmCount = films.Count(f => f.Title == "Inside Out");
            insideOutFilmCount.Should().Be(1);
        }
    }
}