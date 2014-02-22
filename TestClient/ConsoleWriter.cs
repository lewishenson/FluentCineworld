using FluentCineworld.Listings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentCineworld.TestClient
{
    public class ConsoleWriter : IWriter
    {
        public void Output(IEnumerable<Film> films)
        {
            Console.WriteLine("{0} FILMS:", films.Count());

            foreach (var film in films)
            {
                Output(film);
            }
        }

        private void Output(Film film)
        {
            Console.WriteLine("> {0} ({1})", film.Title, film.Rating);

            foreach (var day in film.Days)
            {
                Output(day);
            }
        }

        private void Output(Day day)
        {
            Console.Write("  {0} : ", day.Date.ToShortDateString());

            foreach (var show in day.Shows)
            {
                Console.Write(show.ToString());
                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }
}
