using FluentAssertions;
using FluentCineworld.Listings;
using System;
using System.Linq;
using Xunit;

namespace FluentCineworld.UnitTests.Listings
{
    [Trait("Category", "UnitTest")]
    public class ShowTests
    {
        [Fact]
        public void GivenThereAreNoShows_WhenMerging_ThenNullIsReturned()
        {
            var mergeResult = Show.Merge(Enumerable.Empty<Show>());

            mergeResult.Should().BeNull();
        }

        [Fact]
        public void GivenThereIsOneShow_WhenMerging_ThenThatShowIsReturned()
        {
            var show = new Show();

            var mergeResult = Show.Merge(new[] { show });

            mergeResult.Should().Be(show);
        }

        [Fact]
        public void GivenThereAreMultipleShowsWithDifferentTimes_WhenMerging_ThenAnExceptionShouldBeThrown()
        {
            var show1 = new Show { Time = new DateTime(2014, 1, 1) };
            var show2 = new Show { Time = new DateTime(2014, 1, 2) };

            Action mergeAction = () => Show.Merge(new[] { show1, show2 });

            mergeAction.ShouldThrow<ArgumentException>()
                       .WithMessage("All shows must have the same time.");
        }

        [Fact]
        public void GivenThereAreMultipleShows_WhenMerging_ThenTheResultShouldContainAllValues()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);

            var show1 = new Show { Time = time, AudioDescribed = true, DBox = false, Imax = true, Is2D = false, Is3D = true, Subtitled = false, Vip = true };
            var show2 = new Show { Time = time, AudioDescribed = false, DBox = true, Imax = false, Is2D = true, Is3D = false, Subtitled = true, Vip = false };

            var mergeResult = Show.Merge(new[] { show1, show2 });

            var expectedResult = new Show { Time = time, AudioDescribed = true, DBox = true, Imax = true, Is2D = true, Is3D = true, Subtitled = true, Vip = true };
            mergeResult.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GivenAllPropertiesHaveBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, AudioDescribed = true, DBox = true, Imax = true, Is2D = true, Is3D = true, Subtitled = true, Vip = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (2D, 3D, DBOX, VIP, IMAX, Audio Described, Subtitled)");
        }
    }
}