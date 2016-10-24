using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentCineworld.Listings
{
    public class ListingsMapper : IListingsMapper
    {
        public IEnumerable<Film> Map(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                yield break;
            }

            var dateConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
            var filmDtos = JsonConvert.DeserializeObject<FilmDto[]>(json, dateConverter);
            var films = Map(filmDtos);

            var filmsGroupedByName = films.GroupBy(film => film.Name).ToArray();
            foreach (var filmGroup in filmsGroupedByName)
            {
                if (filmGroup.Count() == 1)
                {
                    yield return filmGroup.Single();
                }
                else
                {
                    yield return MergeFilms(filmGroup);
                }
            }
        }

        private static Film MergeFilms(IEnumerable<Film> films)
        {
            var primaryFilm = films.First();

            foreach (var currentFilm in films.Where(film => film != primaryFilm))
            {
                foreach (var currentDay in currentFilm.Days)
                {
                    var primaryDay = primaryFilm.Days.SingleOrDefault(day => day.Date == currentDay.Date);
                    if (primaryDay == null)
                    {
                        primaryFilm.Days = primaryFilm.Days.Concat(new[] { currentDay })
                                                           .OrderBy(day => day.Date)
                                                           .ToArray();
                    }
                    else
                    {
                        primaryDay.Showings = primaryDay.Showings.Concat(currentDay.Showings)
                                                                 .OrderBy(showing => showing.Time)
                                                                 .ToArray();
                    }
                }
            }

            return primaryFilm;
        }

        private static IEnumerable<Film> Map(IEnumerable<FilmDto> filmDtos)
        {
            foreach (var filmDto in filmDtos)
            {
                yield return Map(filmDto);
            }
        }

        private static Film Map(FilmDto filmDto)
        {
            return new Film
                {
                    Duration = filmDto.Duration,
                    Name = TitleFormatter.Format(filmDto.Name),
                    Rating = filmDto.RatingName,
                    Days = filmDto.Days.Select(Map).ToArray()
                };
        }

        private static Day Map(DayDto dayDto)
        {
            return new Day
                {
                    Date = dayDto.Date,
                    Showings = dayDto.Showings.Select(Map).ToArray()
                };
        }

        private static Showing Map(ShowingDto showingDto)
        {
            var attributes = showingDto.Attributes.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            return new Showing
                {
                    Attributes = attributes,
                    AttributeDescriptions = showingDto.AttributeDescriptions.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries),
                    DisplayText = GenerateDisplayText(showingDto.Time, attributes),
                    Screen = showingDto.Screen,
                    Time = showingDto.Time
                };
        }

        private static string GenerateDisplayText(string time, IEnumerable<string> attributes)
        {
            var attributesPermittedInDisplayText = new HashSet<string>
                {
                    ShowingAttributes.FourDX,
                    ShowingAttributes.Imax,
                    ShowingAttributes.Superscreen,
                    ShowingAttributes.ThreeD,
                    ShowingAttributes.TwoD
                };

            var attributesToUseInDisplayText = attributes.Where(attribute => attributesPermittedInDisplayText.Contains(attribute));

            return string.Format("{0} ({1})", time, string.Join(", ", attributesToUseInDisplayText));
        }
    }
}