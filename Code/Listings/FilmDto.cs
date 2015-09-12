using System.Diagnostics;
using Newtonsoft.Json;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Name = {Name}")]
    public class FilmDto
    {
        [JsonProperty("rdesc")]
        public string RatingDescription { get; set; }

        [JsonProperty("rn")]
        public string RatingName { get; set; }

        [JsonProperty("TYP")]
        public string[] Types { get; set; }

        [JsonProperty("rtn")]
        public string Rtn { get; set; }

        [JsonProperty("dur")]
        public int Duration { get; set; }

        [JsonProperty("rid")]
        public int RatingId { get; set; }

        [JsonProperty("BD")]
        public DayDto[] Days { get; set; }

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("rfn")]
        public string RatingImage { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("TYPD")]
        public string[] TypeDescriptions { get; set; }
    }
}