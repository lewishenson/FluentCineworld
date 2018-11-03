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

            var films = response.Body.Films.Select(this.MapWithoutShowings);

            // TODO: include showings

            return films.ToList();
        }

        private async Task<string> GetJson(Cinema cinema, DateTime date)
        {
            var url = UriGenerator.Listings(cinema, date);
            var json = await _httpClient.GetStringAsync(url).ConfigureAwait(false);

            return json;
        }

        // TODO: extract new class?
        // TODO: format film name
        private Film MapWithoutShowings(FilmDto filmDto)
        {
            return new Film
            {
                Id = filmDto.Id,
                Name = filmDto.Name,
                Duration = filmDto.Length
            };
        }
    }
}