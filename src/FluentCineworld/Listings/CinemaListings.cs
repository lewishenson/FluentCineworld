using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private readonly IFilter _filter;

        public CinemaListings(Cinema cinema, IGetDatesQuery getDatesQuery, IGetFilmsQuery getFilmsQuery, IFilter filter)
        {
            _cinema = cinema ?? throw new ArgumentNullException(nameof(cinema));
            _getDatesQuery = getDatesQuery ?? throw new ArgumentNullException(nameof(getDatesQuery));
            _getFilmsQuery = getFilmsQuery ?? throw new ArgumentNullException(nameof(getFilmsQuery));
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public ICinemaListings ForDayOfWeek(DayOfWeek dayOfWeek)
        {
            _filter.DayOfWeek(dayOfWeek);

            return this;
        }

        public ICinemaListings From(DateOnly from)
        {
            _filter.From(from);

            return this;
        }

        public ICinemaListings To(DateOnly to)
        {
            _filter.To(to);

            return this;
        }

        public async Task<IEnumerable<Film>> RetrieveAsync(CancellationToken cancellationToken)
        {
            var dates = await this.GetDates(cancellationToken).ConfigureAwait(false);
            var films = await this.GetFilms(dates, cancellationToken).ConfigureAwait(false);

            var mergedFilms = this.Merge(films);
            var orderedFilms = mergedFilms.OrderBy(film => film.Name);

            return orderedFilms.ToList();
        }

        private async Task<IEnumerable<DateOnly>> GetDates(CancellationToken cancellationToken)
        {
            var allDates = await _getDatesQuery.ExecuteAsync(_cinema, cancellationToken).ConfigureAwait(false);
            var filteredDates = allDates.Where(_filter.Apply);

            return filteredDates;
        }

        private async Task<IEnumerable<Film>> GetFilms(IEnumerable<DateOnly> dates, CancellationToken cancellationToken)
        {
            var allTasks = new List<Task<IEnumerable<Film>>>();

            foreach (var date in dates)
            {
                var task = _getFilmsQuery.ExecuteAsync(_cinema, date, cancellationToken);
                allTasks.Add(task);
            }

            var films = await Task.WhenAll(allTasks).ConfigureAwait(false);

            return films.SelectMany(film => film);
        }

        private IEnumerable<Film> Merge(IEnumerable<Film> films)
        {
            return films.GroupBy(film => film.Id)
                        .Select(group =>
                        {
                            var allDays = group.SelectMany(f => f.Days)
                                               .OrderBy(day => day.Date);

                            var film = group.First();
                            film.Days = allDays.ToList();

                            return film;
                        });
        }
    }
}