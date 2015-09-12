using System;

namespace FluentCineworld.OldListings
{
    internal static class UriGenerator
    {
        [Obsolete("Do not use, site scrapping no longer works.")]
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