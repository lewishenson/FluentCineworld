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
                this.Output(film);
            }
        }

        private void Output(Film film)
        {
            Console.WriteLine("> {0} ({1})", film.Name, film.Rating);

            foreach (var day in film.Days)
            {
                this.Output(day);
            }
        }

        private void Output(Day day)
        {
            Console.Write("  {0} : ", day.Date.ToString("d"));

            foreach (var show in day.Showings)
            {
                Console.Write(show.DisplayText);
                Console.Write(" ");
            }

            Console.WriteLine();
        }

    }
}