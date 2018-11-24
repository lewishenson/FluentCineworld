using Newtonsoft.Json;

namespace FluentCineworld.Sites
{
    internal class ResponseDto
    {
        [JsonProperty("body")]
        internal BodyDto Body { get; set; }
    }
}