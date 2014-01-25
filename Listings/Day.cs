using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LewisHenson.CineworldCinemas.Listings
{
    [DebuggerDisplay("Date = {Date}")]
    public class Day
    {
        public DateTime Date { get; set; }

        public IEnumerable<Showing> Showings { get; set; }
    }
}
