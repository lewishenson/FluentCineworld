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

            RetrieveDetails(cinema);
            RetrieveFilms(cinema);

            Console.Read();
        }

        private static void RetrieveDetails(Cinema cinema)
        {
            var details = Cineworld.Details(cinema);

            var writer = new DetailsConsoleWriter();
            writer.Output(details);
        }

        private static void RetrieveFilms(Cinema cinema)
        {
            var films = Cineworld.WhatsOn(cinema)
                                 .Retrieve();

            var writer = new FilmConsoleWriter();
            writer.Output(films);
        }

        private static Cinema GetCinema(IEnumerable<string> args)
        {
            var cinemaName = args.FirstOrDefault();

            return string.IsNullOrWhiteSpace(cinemaName) ? Cinema.MiltonKeynes : Enumeration.FromDisplayName<Cinema>(cinemaName);
        }
    }
}