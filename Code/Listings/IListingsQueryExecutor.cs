using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public interface IListingsQueryExecutor
    {
        Task<IEnumerable<Film>> ExecuteAsync();
    }
}