using FluentCineworld.Listings;
using FluentCineworld.Sites;

namespace FluentCineworld
{
    public static class Cineworld
    {
        public static SiteDetails Site(Cinema cinema)
        {
            return new SiteDetailsQueryExecutor(cinema).Execute();
        }

        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            var queryExecutor = new ListingsQueryExecutor(cinema);
            return new CineworldListings(queryExecutor, new Filter());
        }
    }
}