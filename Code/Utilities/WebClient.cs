using System.Net.Http;

namespace FluentCineworld.Utilities
{
    // TODO: Rename [LH]
    public class WebClient : IWebClient
    {
        public string GetContent(string address)
        {
            string content;

            using (var httpClient = new HttpClient())
            {
                content = httpClient.GetStringAsync(address).Result;
            }

            return content;
        }
    }
}