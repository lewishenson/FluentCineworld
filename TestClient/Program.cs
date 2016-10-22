using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentCineworld.TestClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            var cinema = GetCinema(args);

            await RetrieveSite(cinema);
            await RetrieveFilms(cinema);

            Console.Read();
        }

        private static async Task RetrieveSite(Cinema cinema)
        {
            var site = await Cineworld.SiteAsync(cinema);

            var writer = new SiteConsoleWriter();
            writer.Output(site);
        }

        private static async Task RetrieveFilms(Cinema cinema)
        {
            var films = await Cineworld.WhatsOn(cinema)
                                       .RetrieveAsync();

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