using System.Threading.Tasks;
using FluentCineworld.Listings;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;
using FluentCineworld.Sites;
using FluentCineworld.Utilities;

namespace FluentCineworld
{
    public static class Cineworld
    {
        private static readonly IHttpClient HttpClient = new HttpClientWrapper();

        public static async Task<SiteDetails> SiteAsync(Cinema cinema)
        {
            var queryExecutor = new SiteDetailsQueryExecutor(cinema);
            return await queryExecutor.ExecuteAsync();
        }

        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            var getDatesQuery = new GetDatesQuery(HttpClient);
            var filter = new Filter();
            var getFilmsQuery = new GetFilmsQuery(HttpClient);
                        
            return new CineworldListings(cinema, getDatesQuery, filter, getFilmsQuery);
        }
    }
}