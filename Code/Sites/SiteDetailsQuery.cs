using System.Linq;
using FluentCineworld.Utilities;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQuery
    {
        private readonly IWebClient _webClient;
        private readonly ISiteMapper _siteMapper;

        public SiteDetailsQuery(IWebClient webClient, ISiteMapper siteMapper)
        {
            _webClient = webClient;
            _siteMapper = siteMapper;
        }

        public SiteDetails Execute(Cinema cinema)
        {
            var url = UriGenerator.CinemaSites();
            var json = _webClient.GetContent(url);

            var allSites = _siteMapper.Map(json);
            var site = allSites.SingleOrDefault(s => s.Id == cinema.Value);

            return site;
        }
    }
}
