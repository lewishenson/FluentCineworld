using System.Net.Http;

namespace FluentCineworld
{
    public static class Shared
    {
        public static readonly HttpClient HttpClient = new HttpClient();
    }
}
