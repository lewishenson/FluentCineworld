using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetDates
{
    internal class BodyDto
    {
        [JsonProperty("dates")]
        internal IEnumerable<DateTime> Dates { get; set; }
    }
}