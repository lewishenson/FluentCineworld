using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FluentCineworld.Listings.GetDates
{
    internal class BodyDto
    {
        [JsonProperty("dates")]
        internal IEnumerable<DateTime> Dates { get; set; }
    }
}