using System;
using System.Collections.Generic;
using System.Linq;

namespace LewisHenson.CineworldCinemas.Listings
{
    public class CineworldListings : ICineworldListings
    {
        private readonly Cinema _cinema;
        private readonly IScraper<IEnumerable<Movie>> _scraper;
        private readonly Filter _filter;

        internal CineworldListings(Cinema cinema, IScraper<IEnumerable<Movie>> scraper)
        {
            _cinema = cinema;
            _scraper = scraper;
            _filter = new Filter();
        }

        public ICineworldListings ForDayOfWeek(DayOfWeek dayOfWeek)
        {
            _filter.DayOfWeek(dayOfWeek);
            return this;
        }

        public ICineworldListings From(DateTime from)
        {
            _filter.From(from);
            return this;
        }

        public ICineworldListings To(DateTime to)
        {
            _filter.To(to);
            return this;
        }

        public IEnumerable<Movie> Retrieve()
        {
            var url = UrlGenerator.WhatsOn(_cinema);
            var movies = _scraper.Scrape(url);

            movies = _filter.Apply(movies);
            movies = movies.OrderBy(f => f.Name);

            return movies;
        }
    }
}