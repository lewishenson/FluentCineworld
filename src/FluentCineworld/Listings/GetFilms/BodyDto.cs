using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetFilms
{
    internal class BodyDto
    {
        [JsonPropertyName("films")]
        public IEnumerable<FilmDto> Films { get; set; }

        [JsonPropertyName("events")]
        public IEnumerable<EventDto> Events { get; set; }
    }
}