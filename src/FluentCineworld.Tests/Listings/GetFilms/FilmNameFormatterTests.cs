using FluentAssertions;
using FluentCineworld.Listings.GetFilms;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetFilms
{
    public class FilmNameFormatterTests
    {
        [Fact]
        public void Format_GivenNullName_ReturnsEmptyString()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format(null);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Format_GivenEmptyName_ReturnsEmptyString()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format(string.Empty);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Format_GivenWhitespaceName_ReturnsEmptyString()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format(" ");

            result.Should().BeEmpty();
        }

        [Fact]
        public void Format_GivenPaddedName_ReturnsTrimmedName()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format(" The Avengers ");

            result.Should().Be("The Avengers");
        }

        [Fact]
        public void Format_GivenMoviesForJuniorsSuffix_ReturnsTrimmedName()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format("Star Wars Movies For Juniors");

            result.Should().Be("Star Wars");
        }

        [Fact]
        public void Format_GivenSubtitledMoviesForJuniorsSuffix_ReturnsTrimmedName()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format("Toy Story Subtitled Movies For Juniors");

            result.Should().Be("Toy Story");
        }

        [Fact]
        public void Format_GivenRereleaseSuffix_ReturnsTrimmedName()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format("GoldenEye - Rerelease");

            result.Should().Be("GoldenEye");
        }

        [Fact]
        public void Format_GivenAutismFriendlyScreeningPrefix_ReturnsTrimmedName()
        {
            var formatter = new FilmNameFormatter();

            var result = formatter.Format("Autism Friendly Screening: Moana");

            result.Should().Be("Moana");
        }
    }
}