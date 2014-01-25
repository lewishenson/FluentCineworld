using System;
using System.Collections.Generic;
using System.Linq;

namespace LewisHenson.CineworldCinemas.Listings
{
    internal class Filter
    {
        private readonly ICollection<DayOfWeek> _daysOfWeek;
        private DateTime? _from;
        private DateTime? _to;

        public Filter()
        {
            _daysOfWeek = new HashSet<DayOfWeek>();
        }

        public void DayOfWeek(DayOfWeek dayOfWeek)
        {
            if (!_daysOfWeek.Contains(dayOfWeek))
            {
                _daysOfWeek.Add(dayOfWeek);
            }
        }

        public void From(DateTime value)
        {
            _from = value.Date;
        }

        public void To(DateTime value)
        {
            _to = value.Date;
        }

        public IEnumerable<Movie> Apply(IEnumerable<Movie> movies)
        {
            if (_daysOfWeek.Any())
            {
                movies = ApplyDayFilter(movies, d => _daysOfWeek.Contains(d.Date.DayOfWeek));
            }

            if (_from.HasValue)
            {
                movies = ApplyDayFilter(movies, d => d.Date >= _from.Value);
            }

            if (_to.HasValue)
            {
                movies = ApplyDayFilter(movies, d => d.Date <= _to.Value);
            }

            return movies;
        }

        private IEnumerable<Movie> ApplyDayFilter(IEnumerable<Movie> movies, Func<Day, bool> filter)
        {
            var filteredItems = movies.Where(f => f.Days.Any(filter));

            foreach (var item in filteredItems)
            {
                item.Days = item.Days.Where(filter);
            }

            return filteredItems;
        }
    }
}
