﻿using System;
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

                await GetSiteDetails(cineworld, cinema);
                await GetFilms(cineworld, cinema);
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

        private static async Task GetSiteDetails(ICineworld cineworld, Cinema cinema)
        {
            var siteDetails = await cineworld.SiteAsync(cinema);

            new SiteConsoleWriter().Output(siteDetails);
        }

        private static async Task GetFilms(ICineworld cineworld, Cinema cinema)
        {
            var today = DateTime.Now;

            var films = await cineworld.WhatsOn(cinema)
                                       .From(today)
                                       .To(today.AddDays(30))
                                       .RetrieveAsync();

            new FilmConsoleWriter().Output(films);
        }
    }
}