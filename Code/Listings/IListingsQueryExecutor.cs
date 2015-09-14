using System.Collections.Generic;

namespace FluentCineworld.Listings
{
    public interface IListingsQueryExecutor
    {
        IEnumerable<Film> Execute();
    }
}