using System;
using FluentCineworld.Listings;
using FluentCineworld.Utilities;

namespace FluentCineworld.Details
{
    [Obsolete]
    public class CineworldDetails : ICineworldDetails
    {
        private readonly Cinema _cinema;

        private readonly IScraper<CinemaDetails> _scraper;

        internal CineworldDetails(Cinema cinema)
        {
            _cinema = cinema;
            _scraper = new SyndicationDetailsScraper(new WebClient());
        }

        public CinemaDetails Retreive()
        {
            return _scraper.Scrape(_cinema);
        }
    }
}