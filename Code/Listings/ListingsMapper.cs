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
                    Name = filmDto.Name,
                    Rating = filmDto.RatingName,
                    Days = filmDto.Days.Select(Map)
                };
        }

        private static Day Map(DayDto dayDto)
        {
            return new Day
                {
                    Date = dayDto.Date,
                    Showings = dayDto.Showings.Select(Map)
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