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

            if (json.Length == 0)
            {
                return Enumerable.Empty<DateTime>();
            }

            var response = JsonSerializer.Deserialize<ResponseDto>(json);

            return response.Body.Dates;
        }

        private async Task<byte[]> GetJson(Cinema cinema)
        {
            var url = _uriGenerator.ForDatesWithListings(cinema);
            var json = await _httpClient.GetByteArrayAsync(url).ConfigureAwait(false);

            return json;
        }
    }
}