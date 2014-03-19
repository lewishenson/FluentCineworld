namespace FluentCineworld.Listings
{
    internal static class UriGenerator
    {
        public static string WhatsOn(Cinema cinema)
        {
            return "http://www.cineworld.co.uk/whatson?cinema=" + cinema.Value;
        }

        public static string SyndicationListings()
        {
            return "http://www.cineworld.co.uk/syndication/listings.xml";
        }

        public static string CinemaNames()
        {
            return "http://www.cineworld.co.uk/syndication/cinema_names.xml";
        }
    }
}