using System;
using System.Diagnostics;

namespace FluentCineworld.Details
{
    [Obsolete]
    [DebuggerDisplay("Name = {Name}")]
    public class CinemaDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Url { get; set; }
    }
}