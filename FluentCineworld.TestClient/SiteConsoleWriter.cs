using System;
using FluentCineworld.Sites;

namespace FluentCineworld.TestClient
{
    public class SiteConsoleWriter : IWriter<SiteDetails>
    {
        public void Output(SiteDetails item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Address: {0}", item.Address);
            Console.WriteLine("Phone: {0}", item.PhoneNumber);
            Console.WriteLine("URL: {0}", item.Url);
            Console.WriteLine("Longitude: {0}", item.Longitude);
            Console.WriteLine("Latitude: {0}", item.Latitude);

            Console.WriteLine();
        }
    }
}