using System.Threading.Tasks;
using FluentCineworld.Listings;
using FluentCineworld.Sites;

namespace FluentCineworld
{
    public static class Cineworld
    {
        public static async Task<SiteDetails> SiteAsync(Cinema cinema)
        {
            var queryExecutor = new SiteDetailsQueryExecutor(cinema);
            return await queryExecutor.ExecuteAsync();
        }

        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            var queryExecutor = new ListingsQueryExecutor(cinema);
            return new CineworldListings(queryExecutor, new Filter());
        }
    }
}