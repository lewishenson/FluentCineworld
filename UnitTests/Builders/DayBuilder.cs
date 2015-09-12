using System;
using System.Collections.Generic;
using System.Linq;
using FluentCineworld.OldListings;

namespace FluentCineworld.UnitTests.Builders
{
    internal class DayBuilder : IBuilder<Day>
    {
        private readonly ICollection<Func<ShowBuilder, Show>> _showBuilders = new List<Func<ShowBuilder, Show>>();

        private DateTime _date;

        public DayBuilder()
        {
            _date = DateTime.Now.Date;
        }

        public DayBuilder WithDate(DateTime date)
        {
            _date = date;

            return this;
        }

        public DayBuilder WithShow(Func<ShowBuilder, Show> build)
        {
            _showBuilders.Add(build);

            return this;
        }

        public Day Build()
        {
            var day = new Day
                          {
                              Date = _date,
                              Shows = _showBuilders.Select(build => build(new ShowBuilder()))
                          };

            return day;
        }
    }
}