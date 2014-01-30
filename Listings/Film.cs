using System.Collections.Generic;
using System.Diagnostics;

namespace LewisHenson.FluentCineworld.Listings
{
    [DebuggerDisplay("Title = {Title}")]
    public class Film
    {
        public string Title { get; set; }

        public string Rating { get; set; }

        public IEnumerable<Day> Days { get; set; }

        public IDictionary<string, object> Data { get; set; }
    }
}