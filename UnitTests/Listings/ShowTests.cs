using FluentAssertions;
using System;
using System.Linq;
using FluentCineworld.OldListings;
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

            var show1 = new Show { Time = time, AudioDescribed = true, DBox = false, Imax = true, Is2D = false, Is3D = true, Subtitled = false, Vip = true, Is4Dx = true, Superscreen = false };
            var show2 = new Show { Time = time, AudioDescribed = false, DBox = true, Imax = false, Is2D = true, Is3D = false, Subtitled = true, Vip = false, Is4Dx = false, Superscreen = true };

            var mergeResult = Show.Merge(new[] { show1, show2 });

            var expectedResult = new Show { Time = time, AudioDescribed = true, DBox = true, Imax = true, Is2D = true, Is3D = true, Subtitled = true, Vip = true, Is4Dx = true, Superscreen = true };
            mergeResult.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GivenIs2DPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Is2D = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (2D)");
        }

        [Fact]
        public void GivenIs3DPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Is3D = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (3D)");
        }

        [Fact]
        public void GivenAudioDescribedPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, AudioDescribed = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (Audio Described)");
        }

        [Fact]
        public void GivenDBoxPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, DBox = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (DBOX)");
        }

        [Fact]
        public void GivenImaxPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Imax = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (IMAX)");
        }

        [Fact]
        public void GivenSubtitledPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Subtitled = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (Subtitled)");
        }

        [Fact]
        public void GivenVipPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Vip = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (VIP)");
        }

        [Fact]
        public void GivenSuperscreenPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Superscreen = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (Superscreen)");
        }

        [Fact]
        public void Given4DxPropertyHasBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, Is4Dx = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (4DX)");
        }

        [Fact]
        public void GivenAllPropertiesHaveBeenSetToTrue_WhenUsingToString_ThenTheValueIsCorrect()
        {
            var time = new DateTime(2014, 1, 1, 19, 30, 0);
            var show = new Show { Time = time, AudioDescribed = true, DBox = true, Imax = true, Is2D = true, Is3D = true, Subtitled = true, Vip = true, Superscreen = true, Is4Dx = true };

            var result = show.ToString();

            result.ShouldBeEquivalentTo("19:30 (2D, 3D, DBOX, VIP, IMAX, Superscreen, 4DX, Audio Described, Subtitled)");
        }
    }
}