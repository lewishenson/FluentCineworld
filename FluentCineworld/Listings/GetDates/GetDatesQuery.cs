using FluentCineworld.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentCineworld.Listings.GetDates
{
    public class GetDatesQuery : IGetDatesQuery
    {
        private readonly IHttpClient httpClient;

        public GetDatesQuery(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<DateTime>> ExecuteAsync(Cinema cinema)
        {
            var json = await this.GetJson(cinema);

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
            var json = await this.httpClient.GetContentAsync(url);

            return json;
        }
    }
}