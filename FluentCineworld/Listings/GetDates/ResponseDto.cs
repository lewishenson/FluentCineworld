using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetDates
{
    internal class ResponseDto
    {
        [JsonProperty("body")]
        internal BodyDto Body { get; set; }
    }
}