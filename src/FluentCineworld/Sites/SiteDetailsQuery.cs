using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQuery
    {
        private readonly IUriGenerator _uriGenerator;
        private readonly HttpClient _httpClient;

        public SiteDetailsQuery(
            IUriGenerator uriGenerator,
            HttpClient httpClient)
        {
            _uriGenerator = uriGenerator ?? throw new ArgumentNullException(nameof(uriGenerator));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<SiteDetails> ExecuteAsync(Cinema cinema)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var json = await this.GetJson();

            var response = JsonSerializer.Deserialize<ResponseDto>(json);

            var allSites = response.Body.Cinemas.Select(this.Map).ToList();
            var targetSite = allSites.SingleOrDefault(site => site.Id == cinema.Value);

            return targetSite;
        }

        private async Task<string> GetJson()
        {
            var url = _uriGenerator.ForCinemaSites();
            var json = await _httpClient.GetStringAsync(url).ConfigureAwait(false);

            return json;
        }

        private SiteDetails Map(SiteDto siteDto)
        {
            return new SiteDetails
            {
                Address = siteDto.Address,
                DisplayName = siteDto.DisplayName,
                Id = Convert.ToInt32(siteDto.Id),
                Link = siteDto.Link
            };
        }
    }
}