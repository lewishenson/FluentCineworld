using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetDates
{
    internal class ResponseDto
    {
        [JsonPropertyName("body")]
        public BodyDto Body { get; set; }
    }
}
