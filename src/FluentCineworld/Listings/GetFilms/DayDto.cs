using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetFilms
{
    [DebuggerDisplay("Date = {Date}")]
    internal class DayDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("P")]
        public ShowingDto[] Showings { get; set; }

        [JsonPropertyName("d")]
        public long D { get; set; }
    }
}
