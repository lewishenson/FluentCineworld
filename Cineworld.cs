using LewisHenson.CineworldCinemas.Listings;

namespace LewisHenson.CineworldCinemas
{
    public static class Cineworld
    {
        public static ICineworldListings WhatsOn(Cinema cinema)
        {
            return new CineworldListings(cinema);
        }
    }
}