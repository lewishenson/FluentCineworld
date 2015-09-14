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

            RetrieveSite(cinema);
            RetrieveFilms(cinema);

            Console.Read();
        }

        private static void RetrieveSite(Cinema cinema)
        {
            var site = Cineworld.Site(cinema);

            var writer = new SiteConsoleWriter();
            writer.Output(site);
        }

        private static void RetrieveFilms(Cinema cinema)
        {
            var films = Cineworld.WhatsOn(cinema)
                                 .ForDayOfWeek(DayOfWeek.Tuesday)
                                 .ForDayOfWeek(DayOfWeek.Thursday)
                                 .Retrieve()
                                 .ToList();
           
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