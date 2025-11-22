using System;
using FluentAssertions;
using Xunit;

namespace FluentCineworld.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("Toy Story", "Story", "Film", "Toy Film")]
        [InlineData("The LEGO Movie", "Duplo", "Bricks", "The LEGO Movie")]
        [InlineData(
            "The Lion, The Witch & The Wardrobe",
            "the",
            "A",
            "A Lion, A Witch & A Wardrobe"
        )]
        public void Replace_ReplacesStringAsExpected(
            string input,
            string oldValue,
            string newValue,
            string expectedOutput
        )
        {
            var output = input.Replace(oldValue, newValue, StringComparison.OrdinalIgnoreCase);

            output.Should().Be(expectedOutput);
        }
    }
}
