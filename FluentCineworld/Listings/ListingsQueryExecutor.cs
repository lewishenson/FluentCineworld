using System.Collections.Generic;
using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Listings
{
    public class ListingsQueryExecutor : IListingsQueryExecutor
    {
        private readonly Cinema _cinema;
        private readonly ListingsQuery _query;

        internal ListingsQueryExecutor(Cinema cinema)
        {
            _cinema = cinema;
            _query = new ListingsQuery(new HttpClientWrapper(), new ListingsMapper());
        }

        public async Task<IEnumerable<Film>> ExecuteAsync()
        {
            return await _query.ExecuteAsync(_cinema);
        }
    }
}