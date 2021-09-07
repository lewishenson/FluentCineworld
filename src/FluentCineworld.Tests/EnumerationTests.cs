using FluentAssertions;
using Xunit;

namespace FluentCineworld.Tests
{
    public class EnumerationTests
    {
        [Fact]
        public void GetAll_ReturnsExpectedValues()
        {
            var all = Enumeration.GetAll<Colour>();

            all.Should().BeEquivalentTo(Colour.Blue, Colour.Red, Colour.Yellow);
        }

        [Fact]
        public void FromValue_ReturnsExpectedValue()
        {
            var red = Enumeration.FromValue<Colour>("2");

            red.Should().Be(Colour.Red);
        }

        [Fact]
        public void FromDisplayName_ReturnsExpectedValue()
        {
            var yellow = Enumeration.FromDisplayName<Colour>("yellow");

            yellow.Should().Be(Colour.Yellow);
        }

        [Fact]
        public void FromDisplayName_CaseInsensitive_ReturnsExpectedValue()
        {
            var yellow = Enumeration.FromDisplayName<Colour>("YELLOW");

            yellow.Should().Be(Colour.Yellow);
        }

        [Fact]
        public void ToString_ReturnsExpectedValue()
        {
            var toStringValue = Colour.Blue.ToString();

            toStringValue.Should().Be("blue");
        }

        [Fact]
        public void Equals_ReturnsFalseForDifferentTypes()
        {
            var result = Colour.Blue.Equals(this);

            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_ReturnsFalseForSameTypeButDifferentValue()
        {
            var result = Colour.Blue.Equals(Colour.Red);

            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_ReturnsTrueForSameTypeAndValue()
        {
            var result = Colour.Blue.Equals(Colour.Blue);

            result.Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_ReturnsValueHashCode()
        {
            var hashCode = Colour.Blue.GetHashCode();

            hashCode.Should().Be(1.GetHashCode());
        }

        [Theory]
        [InlineData("2", "1", 1)]
        [InlineData("2", "2", 0)]
        [InlineData("2", "3", -1)]
        public void CompareTo_ReturnsExpectedValue(string firstColourValue, string secondColourValue, int expectedResult)
        {
            var firstColour = Enumeration.FromValue<Colour>(firstColourValue);
            var secondColour = Enumeration.FromValue<Colour>(secondColourValue);

            var result = firstColour.CompareTo(secondColour);

            result.Should().Be(expectedResult);
        }

        public class Colour : Enumeration
        {
            public static readonly Colour Blue = new Colour("1", "blue");
            public static readonly Colour Red = new Colour("2", "red");
            public static readonly Colour Yellow = new Colour("3", "yellow");

            protected Colour()
            {
            }

            protected Colour(string id, string name)
                : base(id, name)
            {
            }
        }
    }
}