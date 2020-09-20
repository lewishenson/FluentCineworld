using System.Text.Json.Serialization;

namespace FluentCineworld.Listings.GetFilms
{
    internal class EventDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("filmId")]
        public string FilmId { get; set; }

        [JsonPropertyName("cinemaId")]
        public string CinemaId { get; set; }

        [JsonPropertyName("businessDay")]
        public string BusinessDay { get; set; }

        [JsonPropertyName("eventDateTime")]
        public string EventDateTime { get; set; }

        [JsonPropertyName("attributeIds")]
        public string[] AttributeIds { get; set; }

        [JsonPropertyName("bookingLink")]
        public string BookingLink { get; set; }

        [JsonPropertyName("soldOut")]
        public bool SoldOut { get; set; }
    }
}