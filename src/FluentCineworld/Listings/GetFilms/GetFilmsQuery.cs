using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FluentCineworld.Listings.GetFilms
{
    public class GetFilmsQuery : IGetFilmsQuery
    {
        private readonly HttpClient _httpClient;

        public GetFilmsQuery(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Film>> ExecuteAsync(Cinema cinema, DateTime date)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

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
            var json = await _httpClient.GetStringAsync(url).ConfigureAwait(false);

            return json;
        }

        private Film MapWithoutShowings(FilmDto filmDto)
        {
            // TODO: format film name (extract new class)
            return new Film
            {
                Id = filmDto.Id,
                Name = filmDto.Name,
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