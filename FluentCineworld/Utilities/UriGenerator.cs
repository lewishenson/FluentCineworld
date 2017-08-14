namespace FluentCineworld.Utilities
{
    internal static class UriGenerator
    {
        public static string Listings(Cinema cinema)
        {
            return "https://www.cineworld.co.uk/pgm-site?si=" + cinema.Value;
        }

        public static string CinemaSites()
        {
            return "https://www.cineworld.co.uk/getSites?json=1&max=1000";
        }
    }
}