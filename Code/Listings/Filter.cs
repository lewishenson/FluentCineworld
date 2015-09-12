using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentCineworld.Listings
{
    public class Filter
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

        public IEnumerable<Film> Apply(IEnumerable<Film> films)
        {
            if (_daysOfWeek.Any())
            {
                films = ApplyDayFilter(films, d => _daysOfWeek.Contains(d.Date.DayOfWeek));
            }

            if (_from.HasValue)
            {
                films = ApplyDayFilter(films, d => d.Date >= _from.Value);
            }

            if (_to.HasValue)
            {
                films = ApplyDayFilter(films, d => d.Date <= _to.Value);
            }

            return films;
        }

        private IEnumerable<Film> ApplyDayFilter(IEnumerable<Film> films, Func<Day, bool> filter)
        {
            var filteredItems = films.Where(f => f.Days.Any(filter));

            foreach (var item in filteredItems)
            {
                item.Days = item.Days.Where(filter);
            }

            return filteredItems;
        }
    }
}