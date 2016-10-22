using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public class CineworldListings : ICineworldListings
    {
        private readonly IListingsQueryExecutor _queryExecutor;
        private readonly IFilter _filter;

        internal CineworldListings(IListingsQueryExecutor queryExecutor, IFilter filter)
        {
            _queryExecutor = queryExecutor;
            _filter = filter;
        }

        public ICineworldListings ForDayOfWeek(DayOfWeek dayOfWeek)
        {
            _filter.DayOfWeek(dayOfWeek);
            return this;
        }

        public ICineworldListings From(DateTime from)
        {
            _filter.From(from);
            return this;
        }

        public ICineworldListings To(DateTime to)
        {
            _filter.To(to);
            return this;
        }

        public async Task<IEnumerable<Film>> RetrieveAsync()
        {
            var allFilms = await _queryExecutor.ExecuteAsync();
            var filteredFilms = allFilms.Where(_filter.Apply)
                                        .OrderBy(film => film.Name);

            return filteredFilms;
        }

        public IEnumerable<Film> Retrieve()
        {
            return RetrieveAsync().Result;
        }
    }
}