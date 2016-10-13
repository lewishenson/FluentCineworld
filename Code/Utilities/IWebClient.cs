using System.Threading.Tasks;

namespace FluentCineworld.Utilities
{
    // TODO: Rename [LH]
    public interface IWebClient
    {
        Task<string> GetContentAsync(string address);
    }
}