using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetFilms
{
    [DebuggerDisplay("Date = {Date}")]
    internal class DayDto
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("P")]
        public ShowingDto[] Showings { get; set; }

        [JsonProperty("d")]
        public long D { get; set; }
    }
}