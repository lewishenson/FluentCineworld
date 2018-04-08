using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentCineworld.Listings
{
    public class Filter : IFilter
    {
        private readonly ICollection<DayOfWeek> daysOfWeek;
        private DateTime? from;
        private DateTime? to;

        public Filter()
        {
            daysOfWeek = new HashSet<DayOfWeek>();
        }

        public void DayOfWeek(DayOfWeek value)
        {
            if (!this.daysOfWeek.Contains(value))
            {
                this.daysOfWeek.Add(value);
            }
        }

        public void From(DateTime value)
        {
            this.from = value.Date;
        }

        public void To(DateTime value)
        {
            this.to = value.Date;
        }

        public bool Apply(DateTime date)
        {
            var include = true;

            if (this.daysOfWeek.Any())
            {
                include = this.daysOfWeek.Contains(date.DayOfWeek);
            }

            if (this.from.HasValue)
            {
                include = include && date >= this.from.Value.Date;
            }

            if (this.to.HasValue)
            {
                include = include && date.Date <= this.to.Value.Date;
            }

            return include;
        }
    }
}