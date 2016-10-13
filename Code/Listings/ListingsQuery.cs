using System.Collections.Generic;
using System.Threading.Tasks;
using FluentCineworld.Utilities;

namespace FluentCineworld.Listings
{
    public class ListingsQuery
    {
        private readonly IWebClient _webClient;
        private readonly IListingsMapper _listingsMapper;

        public ListingsQuery(IWebClient webClient, IListingsMapper listingsMapper)
        {
            _webClient = webClient;
            _listingsMapper = listingsMapper;
        }

        public async Task<IEnumerable<Film>> ExecuteAsync(Cinema cinema)
        {
            var url = UriGenerator.Listings(cinema);
            var json = await _webClient.GetContentAsync(url);

            var allListings = _listingsMapper.Map(json);

            return allListings;
        }
    }
}