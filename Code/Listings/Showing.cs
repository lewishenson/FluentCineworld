using System.Collections.Generic;
using System.Diagnostics;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Time = {Time}")]
    public class Showing
    {
        public string Time { get; set; }

        public string Screen { get; set; }

        public IEnumerable<string> Attributes { get; set; }

        public IEnumerable<string> AttributeDescriptions { get; set; }

        public string DisplayText { get; set; }
    }
}