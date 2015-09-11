using System.Diagnostics;

namespace FluentCineworld.Sites
{
    [DebuggerDisplay("Name = {Name}")]
    public class SiteDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Url { get; set; }
    }
}
