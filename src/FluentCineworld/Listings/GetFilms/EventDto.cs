using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetFilms
{
    internal class EventDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("filmId")]
        public string FilmId { get; set; }

        [JsonProperty("cinemaId")]
        public string CinemaId { get; set; }

        [JsonProperty("businessDay")]
        public string BusinessDay { get; set; }

        [JsonProperty("eventDateTime")]
        public string EventDateTime { get; set; }

        [JsonProperty("attributeIds")]
        public string[] AttributeIds { get; set; }

        [JsonProperty("bookingLink")]
        public string BookingLink { get; set; }

        [JsonProperty("soldOut")]
        public bool SoldOut { get; set; }
    }
}