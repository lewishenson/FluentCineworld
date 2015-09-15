using FluentAssertions;
using FluentCineworld.Listings;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class TitleFormatterTests
    {
        [Fact]
        public void GivenThereIsExtraWhitespace_WhenTextIsFormatted_ThenTheExtraWhitespaceIsRemoved()
        {
            var output = TitleFormatter.Format("  output  ");

            output.Should().Be("output");
        }

        [Fact]
        public void GivenThereIsAnUnlimitedScreeningSuffix_WhenTextIsFormatted_ThenTheSuffixIsRemoved1()
        {
            var output = TitleFormatter.Format("The Movie - Unlimited Screening");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsAnUnlimitedScreeningSuffix_WhenTextIsFormatted_ThenTheSuffixIsRemoved2()
        {
            var output = TitleFormatter.Format("The Movie: Unlimited Screening");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsAnUnlimitedScreeningSuffix_WhenTextIsFormatted_ThenTheSuffixIsRemoved3()
        {
            var output = TitleFormatter.Format("The Movie : Cineworld Unlimited Exclusive Show");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsATakeTwoPrefix_WhenTextIsFormatted_ThenThePrefixIsRemoved1()
        {
            var output = TitleFormatter.Format("Take 2 Thursday - The Movie");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsATakeTwoPrefix_WhenTextIsFormatted_ThenThePrefixIsRemoved2()
        {
            var output = TitleFormatter.Format("Take 2 - The Movie");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsA2DPrefix_WhenTextIsFormatted_ThenThePrefixIsRemoved()
        {
            var output = TitleFormatter.Format("2D - The Movie");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsA3DPrefix_WhenTextIsFormatted_ThenThePrefixIsRemoved()
        {
            var output = TitleFormatter.Format("3D - The Movie");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsAMoviesForJuniorsSuffix_WhenTextIsFormatted_ThenTheSuffixIsRemoved1()
        {
            var output = TitleFormatter.Format("The Movie - Movies For Juniors");

            output.Should().Be("The Movie");
        }

        [Fact]
        public void GivenThereIsAMoviesForJuniorsSuffix_WhenTextIsFormatted_ThenTheSuffixIsRemoved2()
        {
            var output = TitleFormatter.Format("The Movie - Subtitled Movies For Juniors");

            output.Should().Be("The Movie");
        }
    }
}