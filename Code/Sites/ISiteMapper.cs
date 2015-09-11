using System.Collections.Generic;

namespace FluentCineworld.Sites
{
    public interface ISiteMapper
    {
        IEnumerable<SiteDetails> Map(string json);
    }
}