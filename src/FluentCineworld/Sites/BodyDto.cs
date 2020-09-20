using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FluentCineworld.Sites
{
    internal class BodyDto
    {
        [JsonPropertyName("cinemas")]
        public List<SiteDto> Cinemas { get; set; }
    }
}