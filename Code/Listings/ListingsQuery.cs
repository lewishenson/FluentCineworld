using System.Collections.Generic;
using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Listings
{
    public class ListingsQuery
    {
        private readonly IHttpClient _httpClient;
        private readonly IListingsMapper _listingsMapper;

        public ListingsQuery(IHttpClient httpClient, IListingsMapper listingsMapper)
        {
            _httpClient = httpClient;
            _listingsMapper = listingsMapper;
        }

        public async Task<IEnumerable<Film>> ExecuteAsync(Cinema cinema)
        {
            var url = UriGenerator.Listings(cinema);
            var json = await _httpClient.GetContentAsync(url);

            var allListings = _listingsMapper.Map(json);

            return allListings;
        }
    }
}