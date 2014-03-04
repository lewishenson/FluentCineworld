using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentCineworld.TestClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cinema = GetCinema(args);
            var films = Cineworld.WhatsOn(cinema).Retrieve();

            var writer = new ConsoleWriter();
            writer.Output(films);

            Console.Read();
        }

        private static Cinema GetCinema(IEnumerable<string> args)
        {
            var cinemaName = args.FirstOrDefault();

            return string.IsNullOrWhiteSpace(cinemaName) ? Cinema.MiltonKeynes : Enumeration.FromDisplayName<Cinema>(cinemaName);
        }
    }
}
