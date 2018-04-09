using FluentCineworld.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentCineworld.Listings.GetFilms
{
    public class GetFilmsQuery : IGetFilmsQuery
    {
        private readonly IHttpClient httpClient;

        public GetFilmsQuery(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Film>> ExecuteAsync(Cinema cinema, DateTime date)
        {
            var json = await this.GetJson(cinema, date).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(json))
            {
                return Enumerable.Empty<Film>();
            }

            var response = JsonConvert.DeserializeObject<ResponseDto>(json);

            var films = response.Body.Films.Select(this.MapWithoutShowings)
                                           .ToDictionary(film => film.Id, film => film);

            this.AssignEventsToFilms(date, films, response.Body.Events);

            return films.Values;
        }

        private async Task<string> GetJson(Cinema cinema, DateTime date)
        {
            var url = UriGenerator.Listings(cinema, date);
            var json = await this.httpClient.GetContentAsync(url).ConfigureAwait(false);

            return json;
        }

        private Film MapWithoutShowings(FilmDto filmDto)
        {
            return new Film
            {
                Id = filmDto.Id,
                Name = FilmNameFormatter.Format(filmDto.Name),
                Duration = filmDto.Length
            };
        }

        private void AssignEventsToFilms(DateTime date, IDictionary<string, Film> films, IEnumerable<EventDto> eventDtos)
        {
            var groupedEvents = eventDtos.GroupBy(eventDto => eventDto.FilmId);

            foreach (var group in groupedEvents)
            {
                var film = films[group.Key];

                var day = this.CreateDay(date, group);
                film.Days = new List<Day> { day };

                film.Rating = this.GetRating(group.First());
            }
        }

        private Day CreateDay(DateTime date, IEnumerable<EventDto> eventDtos)
        {
            return new Day
            {
                Date = date,
                Showings = eventDtos.Select(this.Map).ToList()
            };
        }

        private Showing Map(EventDto eventDto)
        {
            return new Showing
            {
                Time = DateTime.ParseExact(eventDto.EventDateTime, "yyyy-MM-ddTHH:mm:ss", null),
                AttributeIds = eventDto.AttributeIds,
                AttributeTexts = ShowingAttributes.All.Where(attribute => eventDto.AttributeIds.Contains(attribute.Id))
                                                      .Select(attribute => attribute.Text)
                                                      .ToList()
            };
        }

        private string GetRating(EventDto eventDto)
        {
            var ageRestrictionAttribute = ShowingAttributes.AgeRestrictions.FirstOrDefault(attribute => eventDto.AttributeIds.Contains(attribute.Id));

            return ageRestrictionAttribute?.Text;
        }
    }
}