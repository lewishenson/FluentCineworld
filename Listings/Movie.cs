using System.Collections.Generic;
using System.Diagnostics;

namespace LewisHenson.CineworldCinemas.Listings
{
    using System.Text.RegularExpressions;

    [DebuggerDisplay("Name = {Name}")]
    public class Movie
    {
        public string Name { get; set; }

        public string SimpleName
        {
            get
            {
                return GetSimpleName();
            }
        }

        public string Certificate { get; set; }

        public string Synopsis { get; set; }

        public string RunningTime { get; set; }

        public IEnumerable<Day> Days { get; set; }

        private string GetSimpleName()
        {
            var regularExpression = new Regex(@"[^a-zA-Z0-9]");

            return regularExpression.Replace(Name, string.Empty);
        }
    }
}