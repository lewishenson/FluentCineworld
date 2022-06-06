using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
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

        public async Task<IEnumerable<DateOnly>> ExecuteAsync(Cinema cinema, CancellationToken cancellationToken)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var response = await this.GetResponse(cinema, cancellationToken).ConfigureAwait(false);

            return response?.Body == null ? Enumerable.Empty<DateOnly>() : response.Body.Dates.Select(DateOnly.FromDateTime);
        }

        private async Task<ResponseDto> GetResponse(Cinema cinema, CancellationToken cancellationToken)
        {
            var url = _uriGenerator.ForDatesWithListings(cinema);
            var response = await _httpClient.GetFromJsonAsync<ResponseDto>(url, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response;
        }
    }
}
