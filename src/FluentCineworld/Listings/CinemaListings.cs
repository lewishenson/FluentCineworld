using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;

namespace FluentCineworld.Listings
{
    public class CinemaListings : ICinemaListings
    {
        private readonly Cinema _cinema;
        private readonly IGetDatesQuery _getDatesQuery;
        private readonly IGetFilmsQuery _getFilmsQuery;

        public CinemaListings(Cinema cinema, IGetDatesQuery getDatesQuery, IGetFilmsQuery getFilmsQuery)
        {
            _cinema = cinema ?? throw new ArgumentNullException(nameof(cinema));
            _getDatesQuery = getDatesQuery ?? throw new ArgumentNullException(nameof(getDatesQuery));
            _getFilmsQuery = getFilmsQuery ?? throw new ArgumentNullException(nameof(getFilmsQuery));
        }

        // TODO: include filter functionality

        public async Task<IEnumerable<Film>> RetrieveAsync()
        {
            var dates = await this.GetDates().ConfigureAwait(false);
            var films = await this.GetFilms(dates).ConfigureAwait(false);

            // TODO: merge films

            var orderedFilms = films.OrderBy(film => film.Name);

            return orderedFilms.ToList();
        }

        private async Task<IEnumerable<DateTime>> GetDates()
        {
            var dates = await _getDatesQuery.ExecuteAsync(_cinema).ConfigureAwait(false);

            return dates;
        }

        private async Task<IEnumerable<Film>> GetFilms(IEnumerable<DateTime> dates)
        {
            var allTasks = new List<Task<IEnumerable<Film>>>();

            foreach (var date in dates)
            {
                var task = _getFilmsQuery.ExecuteAsync(_cinema, date);
                allTasks.Add(task);
            }

            var films = await Task.WhenAll(allTasks).ConfigureAwait(false);

            return films.SelectMany(film => film);
        }
    }
}