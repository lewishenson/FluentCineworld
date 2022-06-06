using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetFilms
{
    internal class ResponseDto
    {
        [JsonPropertyName("body")]
        public BodyDto Body { get; set; }
    }
}
