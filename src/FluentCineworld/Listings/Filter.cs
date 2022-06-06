using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentCineworld.Listings
{
    public class Filter : IFilter
    {
        private readonly ICollection<DayOfWeek> _daysOfWeek;
        private DateOnly? _from;
        private DateOnly? _to;

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

        public void From(DateOnly value)
        {
            _from = value;
        }

        public void To(DateOnly value)
        {
            _to = value;
        }

        public bool Apply(DateOnly date)
        {
            var include = true;

            if (_daysOfWeek.Any())
            {
                include = _daysOfWeek.Contains(date.DayOfWeek);
            }

            if (_from.HasValue)
            {
                include = include && date >= _from.Value;
            }

            if (_to.HasValue)
            {
                include = include && date <= _to.Value;
            }

            return include;
        }
    }
}