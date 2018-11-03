using System;
using System.Net.Http;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;

namespace FluentCineworld
{
    public class Cineworld : ICineworld
    {
        private readonly HttpClient _httpClient;

        public Cineworld(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public ICinemaListings WhatsOn(Cinema cinema)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var getDatesQuery = new GetDatesQuery(_httpClient);
            var getFilmsQuery = new GetFilmsQuery(_httpClient);
            var cinemaListings = new CinemaListings(cinema, getDatesQuery, getFilmsQuery);

            return cinemaListings;
        }
    }
}