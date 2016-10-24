using System.Threading.Tasks;

namespace FluentCineworld.Utilities
{
    public interface IHttpClient
    {
        Task<string> GetContentAsync(string address);
    }
}