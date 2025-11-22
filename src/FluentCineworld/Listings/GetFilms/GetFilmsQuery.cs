using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FluentCineworld.Listings.GetFilms
{
    public class GetFilmsQuery : IGetFilmsQuery
    {
        private readonly IUriGenerator _uriGenerator;
        private readonly HttpClient _httpClient;
        private readonly IFilmNameFormatter _filmNameFormatter;

        public GetFilmsQuery(
            IUriGenerator uriGenerator,
            HttpClient httpClient,
            IFilmNameFormatter filmNameFormatter
        )
        {
            _uriGenerator = uriGenerator ?? throw new ArgumentNullException(nameof(uriGenerator));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _filmNameFormatter =
                filmNameFormatter ?? throw new ArgumentNullException(nameof(filmNameFormatter));
        }

        public async Task<IEnumerable<Film>> ExecuteAsync(
            Cinema cinema,
            DateOnly date,
            CancellationToken cancellationToken
        )
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var response = await this.GetResponse(cinema, date, cancellationToken)
                .ConfigureAwait(false);

            if (response?.Body == null)
            {
                return Enumerable.Empty<Film>();
            }

            var films = response
                .Body.Films.Select(this.MapWithoutShowings)
                .ToDictionary(film => film.Id, film => film);

            this.AssignEventsToFilms(date, films, response.Body.Events);

            return films.Values;
        }

        private async Task<ResponseDto> GetResponse(
            Cinema cinema,
            DateOnly date,
            CancellationToken cancellationToken
        )
        {
            var url = _uriGenerator.ForListings(cinema, date);
            var response = await _httpClient
                .GetFromJsonAsync<ResponseDto>(url, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return response;
        }

        private Film MapWithoutShowings(FilmDto filmDto)
        {
            return new Film
            {
                Id = filmDto.Id,
                Name = _filmNameFormatter.Format(filmDto.Name),
                Duration = filmDto.Length,
            };
        }

        private void AssignEventsToFilms(
            DateOnly date,
            IDictionary<string, Film> films,
            IEnumerable<EventDto> eventDtos
        )
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

        private Day CreateDay(DateOnly date, IEnumerable<EventDto> eventDtos)
        {
            return new Day { Date = date, Showings = eventDtos.Select(this.Map).ToList() };
        }

        private Showing Map(EventDto eventDto)
        {
            return new Showing
            {
                Time = DateTime.ParseExact(eventDto.EventDateTime, "yyyy-MM-ddTHH:mm:ss", null),
                AttributeIds = eventDto.AttributeIds,
                AttributeTexts = ShowingAttributes
                    .All.Where(attribute => eventDto.AttributeIds.Contains(attribute.Id))
                    .Select(attribute => attribute.Text)
                    .ToList(),
            };
        }

        private string GetRating(EventDto eventDto)
        {
            var ageRestrictionAttribute = ShowingAttributes.AgeRestrictions.FirstOrDefault(
                attribute => eventDto.AttributeIds.Contains(attribute.Id)
            );

            return ageRestrictionAttribute?.Text;
        }
    }
}
