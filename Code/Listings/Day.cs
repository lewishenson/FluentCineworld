using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Date = {Date}")]
    public class Day
    {
        public Day()
        {
        }

        public Day(DateTime date, IEnumerable<Show> shows)
        {
            Date = date;
            Shows = shows;
        }

        public DateTime Date { get; set; }

        public IEnumerable<Show> Shows { get; set; }

        public static Day Merge(IEnumerable<Day> days)
        {
            if (!days.Any())
            {
                return null;
            }

            if (days.Count() == 1)
            {
                return days.Single();
            }

            var firstDay = days.First();
            if (days.Any(f => f.Date != firstDay.Date))
            {
                throw new ArgumentException("All days must have the same date.");
            }

            var mergedShows = days.SelectMany(day => day.Shows).
                                   GroupBy(s => s.Time)
                                  .Select(Show.Merge)
                                  .OrderBy(s => s.Time);

            var mergeResult = new Day(firstDay.Date, mergedShows);

            return mergeResult;
        }
    }
}