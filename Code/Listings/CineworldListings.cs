using System.Collections.Generic;

namespace FluentCineworld.Listings
{
    public class CineworldListings : ICineworldListings
    {
        private readonly ListingsQueryExecutor _queryExecutor;

        internal CineworldListings(Cinema cinema)
        {
            _queryExecutor = new ListingsQueryExecutor(cinema);
        }

        public IEnumerable<Film> Retrieve()
        {
            return _queryExecutor.Execute();
        }
    }
}
