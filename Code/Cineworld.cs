using FluentCineworld.Details;
using FluentCineworld.Listings;

namespace FluentCineworld
{
    public static class Cineworld
    {
        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            return new CineworldListings(cinema);
        }

        public static CinemaDetails Details(Cinema cinema)
        {
            return new CineworldDetails(cinema).Retreive();
        }
    }
}