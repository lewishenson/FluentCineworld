using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetFilms
{
    internal class ResponseDto
    {
        [JsonProperty("body")]
        internal BodyDto Body { get; set; }
    }
}