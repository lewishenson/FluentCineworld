using System;
using FluentCineworld.Details;
using FluentCineworld.Listings;
using FluentCineworld.Sites;

namespace FluentCineworld
{
    public static class Cineworld
    {
        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            return new CineworldListings(cinema);
        }

        [Obsolete]
        public static CinemaDetails Details(Cinema cinema)
        {
            return new CineworldDetails(cinema).Retreive();
        }

        public static SiteDetails Site(Cinema cinema)
        {
            return new SiteDetailsQueryExecutor(cinema).Execute();
        }
    }
}