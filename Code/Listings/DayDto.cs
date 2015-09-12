using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Date = {Date}")]
    public class DayDto
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("P")]
        public ShowingDto[] Showings { get; set; }

        [JsonProperty("d")]
        public long D { get; set; }
    }
}