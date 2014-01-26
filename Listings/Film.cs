using System.Collections.Generic;
using System.Diagnostics;

namespace LewisHenson.CineworldCinemas.Listings
{
    [DebuggerDisplay("Title = {Title}")]
    public class Film
    {
        public string Title { get; set; }

        public string Rating { get; set; }

        public string Synopsis { get; set; }

        public string RunningTime { get; set; }

        public IEnumerable<Day> Days { get; set; }
    }
}