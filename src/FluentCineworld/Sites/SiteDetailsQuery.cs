using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQuery
    {
        private readonly IUriGenerator _uriGenerator;
        private readonly HttpClient _httpClient;

        public SiteDetailsQuery(IUriGenerator uriGenerator, HttpClient httpClient)
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

            var response = await GetResponse().ConfigureAwait(false);

            var allSites = response.Body.Cinemas.Select(this.Map).ToList();
            var targetSite = allSites.SingleOrDefault(site => site.Id == cinema.Id);

            return targetSite;
        }

        private async Task<ResponseDto> GetResponse()
        {
            var url = _uriGenerator.ForCinemaSites();

            var response = await _httpClient.GetFromJsonAsync<ResponseDto>(url).ConfigureAwait(false);

            return response;
        }

        private SiteDetails Map(SiteDto siteDto)
        {
            return new SiteDetails
            {
                Address = siteDto.Address,
                DisplayName = siteDto.DisplayName,
                Id = siteDto.Id,
                Link = siteDto.Link
            };
        }
    }
}