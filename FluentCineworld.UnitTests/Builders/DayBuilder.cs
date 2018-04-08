using System;
using FluentCineworld.Listings;

namespace FluentCineworld.UnitTests.Builders
{
    internal class DayBuilder : IBuilder<Day>
    {
        private DateTime date;

        public DayBuilder()
        {
            this.date = DateTime.Now.Date;
        }

        public DayBuilder WithDate(DateTime value)
        {
            this.date = value;

            return this;
        }

        public Day Build()
        {
            var day = new Day
                {
                    Date = this.date
                };

            return day;
        }
    }
}