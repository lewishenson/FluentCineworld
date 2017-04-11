using System;
using FluentCineworld.Listings;

namespace FluentCineworld.UnitTests.Builders
{
    internal class DayBuilder : IBuilder<Day>
    {
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

        public Day Build()
        {
            var day = new Day
                {
                    Date = _date
                };

            return day;
        }
    }
}