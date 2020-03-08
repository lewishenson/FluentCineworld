using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluentCineworld.Listings.GetDates
{
    public class GetDatesQuery : IGetDatesQuery
    {
        private readonly IUriGenerator _uriGenerator;
        private readonly HttpClient _httpClient;

        public GetDatesQuery(IUriGenerator uriGenerator, HttpClient httpClient)
        {
            _uriGenerator = uriGenerator ?? throw new ArgumentNullException(nameof(uriGenerator));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<DateTime>> ExecuteAsync(Cinema cinema)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var json = await this.GetJson(cinema).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(json))
            {
                return Enumerable.Empty<DateTime>();
            }

            var response = JsonSerializer.Deserialize<ResponseDto>(json);

            return response.Body.Dates;
        }

        private async Task<string> GetJson(Cinema cinema)
        {
            var url = _uriGenerator.ForDatesWithListings(cinema);
            var json = await _httpClient.GetStringAsync(url).ConfigureAwait(false);

            return json;
        }
    }
}