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

        public SiteDetails Execute()
        {
            return _query.Execute(_cinema);
        }
    }
}
