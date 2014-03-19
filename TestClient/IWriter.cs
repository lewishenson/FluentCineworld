using FluentCineworld.Listings;
using System.Collections.Generic;

namespace FluentCineworld.TestClient
{
    public interface IWriter<T>
    {
        void Output(T item);
    }
}