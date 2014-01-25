namespace LewisHenson.CineworldCinemas.Listings
{
    internal static class UrlGenerator
    {
        public static string WhatsOn(Cinema cinema)
        {
            return "http://www.cineworld.co.uk/whatson?cinema=" + cinema.Value;
        }
    }
}