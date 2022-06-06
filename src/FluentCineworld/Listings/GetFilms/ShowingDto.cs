using System.Diagnostics;
using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetFilms
{
    [DebuggerDisplay("Time = {Time}")]
    internal class ShowingDto
    {
        [JsonPropertyName("dt")]
        public long Dt { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("sub")]
        public bool Sub { get; set; }

        [JsonPropertyName("dattr")]
        public string AttributeDescriptions { get; set; }

        [JsonPropertyName("Vt")]
        public int Vt { get; set; }

        [JsonPropertyName("is3d")]
        public bool Is3D { get; set; }

        [JsonPropertyName("attr")]
        public string Attributes { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("dub")]
        public bool Dubbed { get; set; }

        [JsonPropertyName("vn")]
        public string Screen { get; set; }

        [JsonPropertyName("sold")]
        public bool SoldOut { get; set; }
    }
}
