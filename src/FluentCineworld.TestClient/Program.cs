using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentCineworld;

namespace FluentCineworld.TestClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var cineworld = new Cineworld(Shared.HttpClient);

                var cinema = GetCinema(args);
                var films = await cineworld.WhatsOn(cinema).RetrieveAsync();

                new FilmConsoleWriter().Output(films);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static Cinema GetCinema(IEnumerable<string> args)
        {
            var cinemaName = args.FirstOrDefault();

            return string.IsNullOrWhiteSpace(cinemaName)
                    ? Cinema.MiltonKeynes
                    : Enumeration.FromDisplayName<Cinema>(cinemaName);
        }
    }
}