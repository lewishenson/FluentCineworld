using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentCineworld.Listings
{
    public class Filter : IFilter
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

        public bool Apply(Film film)
        {
            var include = true;

            if (_daysOfWeek.Any())
            {
                include = ApplyDayFilter(film, d => _daysOfWeek.Contains(d.Date.DayOfWeek));
            }

            if (_from.HasValue)
            {
                include = include && ApplyDayFilter(film, d => d.Date >= _from.Value);
            }

            if (_to.HasValue)
            {
                include = include && ApplyDayFilter(film, d => d.Date <= _to.Value);
            }

            return include;
        }

        private bool ApplyDayFilter(Film film, Func<Day, bool> filter)
        {
            var include = film.Days.Any(filter);

            if (include)
            {
                film.Days = film.Days.Where(filter);
            }

            return include;
        }
    }
}