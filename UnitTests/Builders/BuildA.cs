namespace FluentCineworld.UnitTests.Builders
{
    internal static class BuildA
    {
        public static FilmBuilder Film()
        {
            return new FilmBuilder();
        }
    }
}