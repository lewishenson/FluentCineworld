using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentCineworld.Listings.GetDates
{
    public class GetDatesQuery : IGetDatesQuery
    {
        private readonly HttpClient _httpClient;

        public GetDatesQuery(HttpClient httpClient)
        {
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

            var dateConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
            var response = JsonConvert.DeserializeObject<ResponseDto>(json, dateConverter);

            return response.Body.Dates;
        }

        private async Task<string> GetJson(Cinema cinema)
        {
            var url = UriGenerator.DatesWithListings(cinema);
            var json = await _httpClient.GetStringAsync(url).ConfigureAwait(false);

            return json;
        }
    }
}