using System;
using System.Collections.Generic;
using System.Linq;
using FluentCineworld.Listings;

namespace FluentCineworld.TestClient
{
    public class FilmConsoleWriter
    {
        public void Output(IEnumerable<Film> films)
        {
            Console.WriteLine("{0} FILMS:", films.Count());

            foreach (var film in films)
            {
                Console.WriteLine($"> {film.Name}");
            }
        }
    }
}