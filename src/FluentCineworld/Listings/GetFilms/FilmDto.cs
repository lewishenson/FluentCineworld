using System.Diagnostics;
using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetFilms
{
    [DebuggerDisplay("Name = {Name}")]
    internal class FilmDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("posterLink")]
        public string PosterLink { get; set; }

        [JsonPropertyName("videoLink")]
        public string VideoLink { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("releaseYear")]
        public string ReleaseYear { get; set; }

        [JsonPropertyName("attributeIds")]
        public string[] AttributeIds { get; set; }
    }
}
