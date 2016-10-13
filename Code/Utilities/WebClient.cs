using System.Net.Http;
using System.Threading.Tasks;

namespace FluentCineworld.Utilities
{
    // TODO: Rename [LH]
    public class WebClient : IWebClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<string> GetContentAsync(string address)
        {
            return await Client.GetStringAsync(address);
        }
    }
}