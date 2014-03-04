﻿using FluentAssertions;
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

            var films = scraper.Scrape(Cinema.MiltonKeynes);

            films.Should().BeEmpty();
        }

        [Fact]
        public void GivenTheCineworldSiteIsAvailable_WhenTheFilmsAreRequestedForACinemaWithFilms_ThenAValidCollectionWillBeReturned()
        {
            var content = File.ReadAllText(@"Data\listings.xml");
            var webClient = Mock.Of<IWebClient>(w => w.GetContent(It.IsAny<string>()) == content);
            var scraper = new SyndicationListingsScraper(webClient);

            var films = scraper.Scrape(Cinema.Aberdeen);

            films.Should().HaveCount(31);

            var nonStopFilm = films.Single(f => f.Title == "Non-Stop");
            nonStopFilm.Rating.Should().Be("12A");
            nonStopFilm.Days.Should().HaveCount(10);

            var firstDay = nonStopFilm.Days.First();
            firstDay.Date.Should().Be(new DateTime(2014, 3, 4));
            firstDay.Shows.Should().HaveCount(2);

            var firstShow = firstDay.Shows.First();
            firstShow.ToString().Should().Be("18:30 (2D, Audio Described)");
        }
    }
}