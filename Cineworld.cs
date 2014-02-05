using FluentCineworld.Listings;

namespace FluentCineworld
{
    public static class Cineworld
    {
        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            return new CineworldListings(cinema);
        }
    }
}