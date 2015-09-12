namespace FluentCineworld.Utilities
{
    internal static class UriGenerator
    {
        public static string Listings(Cinema cinema)
        {
            return "https://www1.cineworld.co.uk/cinemas/pgm-site?si=" + cinema.Value;
        }

        public static string CinemaSites()
        {
            return "http://www1.cineworld.co.uk/getSites?json=1&max=1000";
        }
    }
}