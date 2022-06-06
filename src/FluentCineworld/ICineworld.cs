using System.Threading;
using System.Threading.Tasks;
using FluentCineworld.Listings;
using FluentCineworld.Sites;

namespace FluentCineworld
{
    public interface ICineworld
    {
        ICinemaListings WhatsOn(Cinema cinema);

        Task<SiteDetails> SiteAsync(Cinema cinema, CancellationToken cancellationToken = default);
    }
}