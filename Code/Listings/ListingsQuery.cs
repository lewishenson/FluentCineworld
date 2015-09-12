using System.Collections.Generic;
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

        public IEnumerable<Film> Execute(Cinema cinema)
        {
            var url = UriGenerator.Listings(cinema);
            var json = _webClient.GetContent(url);

            var allListings = _listingsMapper.Map(json);

            return allListings;
        }
    }
}