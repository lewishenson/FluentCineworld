using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public interface ICinemaListings
    {
         Task<IEnumerable<Film>> RetrieveAsync();
    }
}