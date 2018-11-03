using System.Diagnostics;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Name = {Name}")]
    public class Film
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }
    }
}