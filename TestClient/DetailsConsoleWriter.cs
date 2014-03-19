using FluentCineworld.Details;
using System;

namespace FluentCineworld.TestClient
{
    public class DetailsConsoleWriter : IWriter<CinemaDetails>
    {
        public void Output(CinemaDetails item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Address: {0}, Post Code: {1}", item.Address, item.PostCode);
            Console.WriteLine("Phone: {0}", item.Phone);
            Console.WriteLine("URL: {0}", item.Url);

            Console.WriteLine();
        }
    }
}