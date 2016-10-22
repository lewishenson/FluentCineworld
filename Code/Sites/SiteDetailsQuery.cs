using System.Linq;
using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQuery
    {
        private readonly IHttpClient _httpClient;
        private readonly ISiteMapper _siteMapper;

        public SiteDetailsQuery(IHttpClient httpClient, ISiteMapper siteMapper)
        {
            _httpClient = httpClient;
            _siteMapper = siteMapper;
        }

        public async Task<SiteDetails> ExecuteAsync(Cinema cinema)
        {
            var url = UriGenerator.CinemaSites();
            var json = await _httpClient.GetContentAsync(url);

            var allSites = _siteMapper.Map(json);
            var site = allSites.SingleOrDefault(s => s.Id == cinema.Value);

            return site;
        }
    }
}