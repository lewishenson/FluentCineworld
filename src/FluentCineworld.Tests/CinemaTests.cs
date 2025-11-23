using System;
using AwesomeAssertions;
using Xunit;

namespace FluentCineworld.Tests;

public class CinemaTests
{
    [Fact]
    public void GetById_GivenValidId_ThenCinemaIsReturned()
    {
        var cinema = Cinema.GetById("119");

        cinema.Should().BeSameAs(Cinema.Barnsley);
    }

    [Fact]
    public void GetById_GivenInvalidId_ThenOutOfRangeExceptionThrown() =>
        FluentActions
            .Invoking(() => Cinema.GetById("999"))
            .Should()
            .ThrowExactly<ArgumentOutOfRangeException>()
            .WithParameterName("id");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetById_GivenMissingId_ThenOutOfRangeExceptionThrown(string id) =>
        FluentActions
            .Invoking(() => Cinema.GetById(id))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithParameterName("id");
}
