using Newtonsoft.Json;
using System.Collections.Generic;

namespace FluentCineworld.Listings.GetFilms
{
    internal class BodyDto
    {
        [JsonProperty("films")]
        internal IEnumerable<FilmDto> Films { get; set; }

        [JsonProperty("events")]
        internal IEnumerable<EventDto> Events { get; set; }
    }
}