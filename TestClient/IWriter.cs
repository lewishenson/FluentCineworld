using FluentCineworld.Listings;
using System.Collections.Generic;

namespace FluentCineworld.TestClient
{
    public interface IWriter
    {
        void Output(IEnumerable<Film> films);
    }
}