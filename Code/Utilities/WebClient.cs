using System.Net.Http;

namespace FluentCineworld.Utilities
{
    // TODO: Rename [LH]
    public class WebClient : IWebClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public string GetContent(string address)
        {
            return Client.GetStringAsync(address).Result;
        }
    }
}