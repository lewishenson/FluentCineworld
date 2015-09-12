using System.Collections.Generic;

namespace FluentCineworld.Listings
{
    public interface IListingsMapper
    {
        IEnumerable<Film> Map(string json);
    }
}