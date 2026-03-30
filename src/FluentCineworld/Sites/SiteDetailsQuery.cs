using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FluentCineworld.Sites
{
    public class SiteDetailsQuery(IUriGenerator uriGenerator, HttpClient httpClient)
    {
        private readonly IUriGenerator _uriGenerator =
            uriGenerator ?? throw new ArgumentNullException(nameof(uriGenerator));
        private readonly HttpClient _httpClient =
            httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        public async Task<SiteDetails> ExecuteAsync(
            Cinema cinema,
            CancellationToken cancellationToken
        )
        {
            ArgumentNullException.ThrowIfNull(cinema);

            var response = await GetResponse(cancellationToken).ConfigureAwait(false);

            var allSites = response.Body.Cinemas.Select(Map).ToList();
            var targetSite = allSites.SingleOrDefault(site => site.Id == cinema.Id);

            return targetSite;
        }

        private async Task<ResponseDto> GetResponse(CancellationToken cancellationToken)
        {
            var url = _uriGenerator.ForCinemaSites();

            var response = await _httpClient
                .GetFromJsonAsync<ResponseDto>(url, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response;
        }

        private SiteDetails Map(SiteDto siteDto) =>
            new()
            {
                Address = siteDto.Address,
                DisplayName = siteDto.DisplayName,
                Id = siteDto.Id,
                Link = siteDto.Link,
            };
    }
}
