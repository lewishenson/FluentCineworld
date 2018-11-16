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

        public void DayOfWeek(DayOfWeek value)
        {
            if (!_daysOfWeek.Contains(value))
            {
                _daysOfWeek.Add(value);
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

        public bool Apply(DateTime date)
        {
            var include = true;

            if (_daysOfWeek.Any())
            {
                include = _daysOfWeek.Contains(date.DayOfWeek);
            }

            if (_from.HasValue)
            {
                include = include && date >= _from.Value.Date;
            }

            if (_to.HasValue)
            {
                include = include && date.Date <= _to.Value.Date;
            }

            return include;
        }
    }
}