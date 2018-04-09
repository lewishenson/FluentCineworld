using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Time = {Time}")]
    public class Showing
    {
        public DateTime Time { get; set; }

        public IEnumerable<string> AttributeIds { get; set; }

        public IEnumerable<string> AttributeTexts { get; set; }

        public string DisplayText
        {
            get
            {
                var attributes = string.Join(", ", this.AttributeTexts);

                return $"{this.Time.ToString("HH:mm")} ({attributes})";
            }
        }
    }
}