using FluentAssertions;
using FluentCineworld.Listings.GetFilms;
using Xunit;

namespace FluentCineworld.Tests.Listings.GetFilms
{
    public class FilmNameFormatterTests
    {
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("Iron Man", "Iron Man")]
        [InlineData("IRON MAN", "IRON MAN")]
        [InlineData(" The Incredible Hulk ", "The Incredible Hulk")]
        [InlineData(" THE INCREDIBLE HULK ", "THE INCREDIBLE HULK")]
        [InlineData("Iron Man 2 Movies For Juniors", "Iron Man 2")]
        [InlineData("IRON MAN 2 MOVIES FOR JUNIORS", "IRON MAN 2")]
        [InlineData("Thor: Movies For Juniors", "Thor")]
        [InlineData("THOR: MOVIES FOR JUNIORS", "THOR")]
        [InlineData("Captain America: The First Avenger - Movies For Juniors", "Captain America: The First Avenger")]
        [InlineData("CAPTAIN AMERICA: THE FIRST AVENGER - MOVIES FOR JUNIORS", "CAPTAIN AMERICA: THE FIRST AVENGER")]
        [InlineData("The Avengers Subtitled Movies For Juniors", "The Avengers")]
        [InlineData("THE AVENGERS SUBTITLED MOVIES FOR JUNIORS", "THE AVENGERS")]
        [InlineData("Iron Man 3: Subtitled Movies For Juniors", "Iron Man 3")]
        [InlineData("IRON MAN 3: SUBTITLED MOVIES FOR JUNIORS", "IRON MAN 3")]
        [InlineData("Thor: The Dark World - Subtitled Movies For Juniors", "Thor: The Dark World")]
        [InlineData("THOR: THE DARK WORLD - SUBTITLED MOVIES FOR JUNIORS", "THOR: THE DARK WORLD")]
        [InlineData("Captain America: The Winter Soldier - Rerelease", "Captain America: The Winter Soldier")]
        [InlineData("CAPTAIN AMERICA: THE WINTER SOLDIER - RERELEASE", "CAPTAIN AMERICA: THE WINTER SOLDIER")]
        [InlineData("Guardians of the Galaxy: Unlimited Screening", "Guardians of the Galaxy")]
        [InlineData("GUARDIANS OF THE GALAXY: UNLIMITED SCREENING", "GUARDIANS OF THE GALAXY")]
        [InlineData("Autism Friendly Screening: Avengers: Age of Ultron", "Avengers: Age of Ultron")]
        [InlineData("AUTISM FRIENDLY SCREENING: AVENGERS: AGE OF ULTRON", "AVENGERS: AGE OF ULTRON")]
        [InlineData("Classic Movies: Ant-Man", "Ant-Man")]
        [InlineData("CLASSIC MOVIES: ANT-MAN", "ANT-MAN")]
        [InlineData("M4J : Captain America: Civil War", "Captain America: Civil War")]
        [InlineData("M4J : CAPTAIN AMERICA: CIVIL WAR", "CAPTAIN AMERICA: CIVIL WAR")]
        [InlineData("(2D) Doctor Strange", "Doctor Strange")]
        [InlineData("(2D) DOCTOR STRANGE", "DOCTOR STRANGE")]
        [InlineData("(M4J) Guardians of the Galaxy Vol. 2", "Guardians of the Galaxy Vol. 2")]
        [InlineData("(M4J) GUARDIANS OF THE GALAXY VOL. 2", "GUARDIANS OF THE GALAXY VOL. 2")]
        public void Format_GivenInput_ReturnsExpectedOutput(string input, string expectedOutput)
        {
            var formatter = new FilmNameFormatter();

            var output = formatter.Format(input);

            output.Should().Be(expectedOutput);
        }
    }
}
