using System.Linq;
using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQuery
    {
        private readonly IHttpClient httpClient;
        private readonly ISiteMapper siteMapper;

        public SiteDetailsQuery(IHttpClient httpClient, ISiteMapper siteMapper)
        {
            this.httpClient = httpClient;
            this.siteMapper = siteMapper;
        }

        public async Task<SiteDetails> ExecuteAsync(Cinema cinema)
        {
            var url = UriGenerator.CinemaSites();
            var json = await this.httpClient.GetContentAsync(url).ConfigureAwait(false);

            var allSites = this.siteMapper.Map(json);
            var targetSite = allSites.SingleOrDefault(site => site.Id == cinema.Value);

            return targetSite;
        }
    }
}