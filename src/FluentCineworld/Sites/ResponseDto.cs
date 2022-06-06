using System.Text.Json.Serialization;

namespace FluentCineworld.Sites
{
    internal class ResponseDto
    {
        [JsonPropertyName("body")]
        public BodyDto Body { get; set; }
    }
}
