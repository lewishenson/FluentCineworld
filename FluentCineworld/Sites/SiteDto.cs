using System.Diagnostics;
using Newtonsoft.Json;

namespace FluentCineworld.Sites
{
    [DebuggerDisplay("Name = {Name}")]
    internal class SiteDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("excode")]
        public int ExCode { get; set; }

        [JsonProperty("idx")]
        public int Index { get; set; }

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("addr")]
        public string Address { get; set; }

        [JsonProperty("pn")]
        public string PhoneNumber { get; set; }

        [JsonProperty("long")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}