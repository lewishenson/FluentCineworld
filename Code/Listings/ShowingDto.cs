using System.Diagnostics;
using Newtonsoft.Json;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Time = {Time}")]
    public class ShowingDto
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("sub")]
        public bool Sub { get; set; }

        [JsonProperty("dattr")]
        public string AttributeDescriptions { get; set; }

        [JsonProperty("Vt")]
        public int Vt { get; set; }

        [JsonProperty("is3d")]
        public bool Is3D { get; set; }

        [JsonProperty("attr")]
        public string Attributes { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("dub")]
        public bool Dubbed { get; set; }

        [JsonProperty("vn")]
        public string Screen { get; set; }

        [JsonProperty("sold")]
        public bool SoldOut { get; set; }
    }
}