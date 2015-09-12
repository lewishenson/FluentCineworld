using System.Collections.Generic;
using FluentCineworld.Utilities;

namespace FluentCineworld.Listings
{
    public class ListingsQueryExecutor
    {
        private readonly Cinema _cinema;
        private readonly ListingsQuery _query;

        internal ListingsQueryExecutor(Cinema cinema)
        {
            _cinema = cinema;
            _query = new ListingsQuery(new WebClient(), new ListingsMapper());
        }

        public IEnumerable<Film> Execute()
        {
            return _query.Execute(_cinema);
        }
    }
}