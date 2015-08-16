using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class FilmTests
    {
        [Fact]
        public void GivenThereAreNoFilms_WhenMerging_ThenNullIsReturned()
        {
            var mergeResult = Film.Merge(Enumerable.Empty<Film>());

            mergeResult.Should().BeNull();
        }

        [Fact]
        public void GivenThereIsOneFilm_WhenMerging_ThenThatFilmIsReturned()
        {
            var film = new Film();

            var mergeResult = Film.Merge(new[] { film });

            mergeResult.Should().Be(film);
        }

        [Fact]
        public void GivenThereAreMultipleFilmsWithDifferentTitles_WhenMerging_ThenAnExceptionShouldBeThrown()
        {
            var film1 = new Film { Title = "The Dark Knight" };
            var film2 = new Film { Title = "The Dark Knight Rises" };

            Action mergeAction = () => Film.Merge(new[] { film1, film2 });

            mergeAction.ShouldThrow<ArgumentException>()
                       .WithMessage("All films must have the same title.");
        }

        [Fact]
        public void GivenThereAreMultipleFilmsWithDifferentRatings_WhenMerging_ThenAnExceptionShouldBeThrown()
        {
            var film1 = new Film { Rating = "12A" };
            var film2 = new Film { Rating = "PG" };

            Action mergeAction = () => Film.Merge(new[] { film1, film2 });

            mergeAction.ShouldThrow<ArgumentException>()
                       .WithMessage("All films must have the same rating.");
        }

        [Fact]
        public void GivenThereAreMultipleFilms_WhenMerging_ThenTheResultShouldContainAllDataValues()
        {
            var film1 = new Film
            {
                Title = "Ant-Man",
                Rating = "12A",
                Data = new Dictionary<string, object> { { "Runtime", 117 } }
            };

            var film2 = new Film
            {
                Title = "Ant-Man",
                Rating = "12A",
                Data = new Dictionary<string, object> { { "Score", 9 } }
            };

            var mergeResult = Film.Merge(new[] { film1, film2 });

            var expectedResult = new Film
            {
                Title = "Ant-Man",
                Rating = "12A",
                Data = new Dictionary<string, object> { { "Runtime", 117 }, { "Score", 9 } },
                Days = new List<Day>()
            };

            mergeResult.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GivenThereAreMultipleFilms_WhenMerging_ThenTheResultShouldContainAllDays()
        {
            var film1 = new Film
            {
                Title = "Ant-Man",
                Rating = "12A",
                Days = new List<Day>
                    {
                        new Day { Date = new DateTime(2015, 1, 1)}
                    }
            };

            var film2 = new Film
            {
                Title = "Ant-Man",
                Rating = "12A",
                Data = new Dictionary<string, object>(),
                Days = new List<Day>
                    {
                        new Day { Date = new DateTime(2015, 1, 2) }
                    }
            };

            var mergeResult = Film.Merge(new[] { film1, film2 });

            var expectedResult = new Film
            {
                Title = "Ant-Man",
                Rating = "12A",
                Data = new Dictionary<string, object>(),
                Days = new List<Day>
                    {
                        new Day { Date = new DateTime(2015, 1, 1) },
                        new Day { Date = new DateTime(2015, 1, 2) }
                    }
            };

            mergeResult.ShouldBeEquivalentTo(expectedResult);
        }
    }
}
