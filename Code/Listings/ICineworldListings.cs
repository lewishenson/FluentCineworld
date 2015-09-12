using System.Collections.Generic;

namespace FluentCineworld.Listings
{
    public interface ICineworldListings
    {
        IEnumerable<Film> Retrieve();
    }
}