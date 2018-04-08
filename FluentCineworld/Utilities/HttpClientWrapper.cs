﻿using System.Net.Http;
using System.Threading.Tasks;

namespace FluentCineworld.Utilities
{
    public class HttpClientWrapper : IHttpClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<string> GetContentAsync(string address)
        {
            return await Client.GetStringAsync(address).ConfigureAwait(false);
        }
    }
}