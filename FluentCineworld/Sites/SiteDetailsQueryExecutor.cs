using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQueryExecutor
    {
        private readonly Cinema cinema;
        private readonly SiteDetailsQuery query;

        internal SiteDetailsQueryExecutor(Cinema cinema)
        {
            this.cinema = cinema;
            this.query = new SiteDetailsQuery(new HttpClientWrapper(), new SiteMapper());
        }

        public async Task<SiteDetails> ExecuteAsync()
        {
            return await this.query.ExecuteAsync(this.cinema);
        }
    }
}