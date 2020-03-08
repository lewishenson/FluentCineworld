using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetDates
{
    internal class BodyDto
    {
        [JsonPropertyName("dates")]
        public IEnumerable<DateTime> Dates { get; set; }
    }
}