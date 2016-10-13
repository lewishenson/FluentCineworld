using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQueryExecutor
    {
        private readonly Cinema _cinema;
        private readonly SiteDetailsQuery _query;

        internal SiteDetailsQueryExecutor(Cinema cinema)
        {
            _cinema = cinema;
            _query = new SiteDetailsQuery(new WebClient(), new SiteMapper());
        }

        public async Task<SiteDetails> ExecuteAsync()
        {
            return await _query.ExecuteAsync(_cinema);
        }
    }
}