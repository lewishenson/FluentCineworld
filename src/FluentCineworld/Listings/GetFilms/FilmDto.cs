using System.Diagnostics;
using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetFilms
{
    [DebuggerDisplay("Name = {Name}")]
    internal class FilmDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("posterLink")]
        public string PosterLink { get; set; }

        [JsonProperty("videoLink")]
        public string VideoLink { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("releaseYear")]
        public string ReleaseYear { get; set; }

        [JsonProperty("attributeIds")]
        public string[] AttributeIds { get; set; }
    }
}