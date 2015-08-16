using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;

namespace FluentCineworld.Listings
{
    [DebuggerDisplay("Title = {Title}")]
    public class Film
    {
        public string Title { get; set; }

        public string Rating { get; set; }

        public IEnumerable<Day> Days { get; set; }

        public IDictionary<string, object> Data { get; set; }

        public static Film Merge(IEnumerable<Film> films)
        {
            if (!films.Any())
            {
                return null;
            }

            if (films.Count() == 1)
            {
                return films.Single();
            }

            var firstFilm = films.First();
            if (films.Any(f => f.Title != firstFilm.Title))
            {
                throw new ArgumentException("All films must have the same title.");
            }

            if (films.Any(f => f.Rating != firstFilm.Rating))
            {
                throw new ArgumentException("All films must have the same rating.");
            }

            var mergedData = new Dictionary<string, object>();
            foreach (var data in films.Where(f => f.Data != null)
                                      .SelectMany(film => film.Data))
            {
                mergedData[data.Key] = data.Value;
            }

            var mergeResult = new Film
                {
                    Title = firstFilm.Title,
                    Rating = firstFilm.Rating,
                    Data = mergedData,
                    Days = films.Where(f => f.Days != null)
                                .SelectMany(f => f.Days)
                                .GroupBy(d => d.Date)
                                .Select(Day.Merge)
                                .ToList()
                };

            return mergeResult;
        }
    }
}