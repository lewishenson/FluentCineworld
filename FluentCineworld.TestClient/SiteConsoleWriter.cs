using System;
using FluentCineworld.Sites;

namespace FluentCineworld.TestClient
{
    public class SiteConsoleWriter : IWriter<SiteDetails>
    {
        public void Output(SiteDetails item)
        {
            Console.WriteLine("Id: {0}", item.Id);
            Console.WriteLine("Display Name: {0}", item.DisplayName);
            Console.WriteLine("Address: {0}", item.Address);
            Console.WriteLine("Link: {0}", item.Link);

            Console.WriteLine();
        }
    }
}