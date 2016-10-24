using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FluentAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class ListingsMapperTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenThereIsNoData_WhenMapping_ThenAnEmptyCollectionIsReturned(string json)
        {
            var listingsMapper = new ListingsMapper();

            var actualCollection = listingsMapper.Map(json);

            actualCollection.ShouldBeEquivalentTo(Enumerable.Empty<Film>());
        }

        [Fact]
        public void GivenThereAreMultipleFilmsInTheData_WhenMapping_ThenMultipleFilmsShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data\listings.json");
            var listingsMapper = new ListingsMapper();

            var actualCollection = listingsMapper.Map(json);

            actualCollection.Count().ShouldBeEquivalentTo(28);
        }

        [Fact]
        public void GivenThereIsValidData_WhenMapping_ThenAPopulatedFilmDetailsShouldBeReturned()
        {
            var json = File.ReadAllText(@"Data\listings.json");
            var listingsMapper = new ListingsMapper();

            var actual = listingsMapper.Map(json).First();

            var expected = new Film
                {
                    Duration = 165,
                    Name = "Aida On Sydney Harbour",
                    Rating = "PG",
                    Days = new Collection<Day>
                        {
                            new Day
                                {
                                    Date = new DateTime(2015, 9, 15),
                                    Showings = new Collection<Showing>
                                        {
                                            new Showing
                                                {
                                                    Attributes = new Collection<string> { "2D", "AC", "AD", "3D", "SS", "4DX", "M4J", "ST", "PRE", "IMAX" },
                                                    AttributeDescriptions = new Collection<string> { "2D", "Alternative Content", "Audio Described", "3D", "Superscreen", "4DX", "Movies for Juniors", "Subtitled", "Unlimited Preview", "IMAX" },
                                                    DisplayText = "18:30 (2D, 3D, SS, 4DX, IMAX)",
                                                    Screen = "Screen 4",
                                                    Time = "18:30"
                                                }
                                        }
                                }
                        }
                };

            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void GivenThereAreDuplicates_WhenMapping_ThenTheDuplicatesShouldBeMerged()
        {
            var json = File.ReadAllText(@"Data\listingsWithDuplicates.json");
            var listingsMapper = new ListingsMapper();

            var actual = listingsMapper.Map(json).Single();

            var expected = new Film
            {
                Duration = 165,
                Name = "Aida On Sydney Harbour",
                Rating = "PG",
                Days = new Collection<Day>
                        {
                            new Day
                                {
                                    Date = new DateTime(2015, 9, 15),
                                    Showings = new Collection<Showing>
                                        {
                                            new Showing
                                                {
                                                    Attributes = new Collection<string> { "2D", "AC", "AD", "3D", "SS", "4DX", "M4J", "ST", "PRE", "IMAX" },
                                                    AttributeDescriptions = new Collection<string> { "2D", "Alternative Content", "Audio Described", "3D", "Superscreen", "4DX", "Movies for Juniors", "Subtitled", "Unlimited Preview", "IMAX" },
                                                    DisplayText = "18:30 (2D, 3D, SS, 4DX, IMAX)",
                                                    Screen = "Screen 4",
                                                    Time = "18:30"
                                                },
                                            new Showing
                                                {
                                                    Attributes = new Collection<string> { "2D", "AC", "AD", "3D", "SS", "4DX", "M4J", "ST", "PRE", "IMAX" },
                                                    AttributeDescriptions = new Collection<string> { "2D", "Alternative Content", "Audio Described", "3D", "Superscreen", "4DX", "Movies for Juniors", "Subtitled", "Unlimited Preview", "IMAX" },
                                                    DisplayText = "19:30 (2D, 3D, SS, 4DX, IMAX)",
                                                    Screen = "Screen 5",
                                                    Time = "19:30"
                                                }
                                        }
                                },
                            new Day
                                {
                                    Date = new DateTime(2015, 9, 16),
                                    Showings = new Collection<Showing>
                                        {
                                            new Showing
                                                {
                                                    Attributes = new Collection<string> { "2D", "AC", "AD", "3D", "SS", "4DX", "M4J", "ST", "PRE", "IMAX" },
                                                    AttributeDescriptions = new Collection<string> { "2D", "Alternative Content", "Audio Described", "3D", "Superscreen", "4DX", "Movies for Juniors", "Subtitled", "Unlimited Preview", "IMAX" },
                                                    DisplayText = "19:30 (2D, 3D, SS, 4DX, IMAX)",
                                                    Screen = "Screen 5",
                                                    Time = "19:30"
                                                }
                                        }
                                }
                        }
            };

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}