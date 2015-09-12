namespace FluentCineworld.Utilities
{
    public class WebClient : IWebClient
    {
        private const string ChromeUserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.11 Safari/537.36";

        private readonly string _userAgent;

        public WebClient()
            : this(ChromeUserAgent)
        {
        }

        public WebClient(string userAgent)
        {
            _userAgent = userAgent;
        }

        public string UserAgent
        {
            get
            {
                return _userAgent;
            }
        }

        public string GetContent(string address)
        {
            string content;

            using (var webClient = CreateWebClient())
            {
                content = webClient.DownloadString(address);
            }

            return content;
        }

        private System.Net.WebClient CreateWebClient()
        {
            return new System.Net.WebClient
                {
                    Headers = { ["UserAgent"] = _userAgent }
                };
        }
    }
}