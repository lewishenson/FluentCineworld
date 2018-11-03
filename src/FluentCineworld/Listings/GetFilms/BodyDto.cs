using System.Collections.Generic;
using Newtonsoft.Json;

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