using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentCineworld.Sites
{
    internal class BodyDto
    {
        [JsonProperty("cinemas")]
        internal List<SiteDto> Cinemas { get; set; }
    }
}