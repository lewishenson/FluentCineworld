using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;
using FluentCineworld.Sites;

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

            var uriGenerator = new Listings.UriGenerator();
            var getDatesQuery = new GetDatesQuery(uriGenerator, _httpClient);
            var filmNameFormatter = new FilmNameFormatter();
            var getFilmsQuery = new GetFilmsQuery(uriGenerator, _httpClient, filmNameFormatter);
            var filter = new Filter();
            var cinemaListings = new CinemaListings(cinema, getDatesQuery, getFilmsQuery, filter);

            return cinemaListings;
        }

        public async Task<SiteDetails> SiteAsync(Cinema cinema)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var uriGenerator = new Sites.UriGenerator();
            var siteDetailsQuery = new SiteDetailsQuery(uriGenerator, _httpClient);
            var siteDetails = await siteDetailsQuery.ExecuteAsync(cinema).ConfigureAwait(false);

            return siteDetails;
        }
    }
}