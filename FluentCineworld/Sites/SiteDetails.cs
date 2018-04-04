using System.Diagnostics;

namespace FluentCineworld.Sites
{
    [DebuggerDisplay("DisplayName = {DisplayName}")]
    public class SiteDetails
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string Address { get; set; }

        public string Link { get; set; }
    }
}   