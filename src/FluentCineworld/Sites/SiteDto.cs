using System.Diagnostics;
using System.Text.Json.Serialization;

namespace FluentCineworld.Sites
{
    [DebuggerDisplay("DisplayName = {DisplayName}")]
    internal class SiteDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("bookingUrl")]
        public string BookingUrl { get; set; }

        [JsonPropertyName("blockOnlineSales")]
        public bool BlockOnlineSales { get; set; }
    }
}