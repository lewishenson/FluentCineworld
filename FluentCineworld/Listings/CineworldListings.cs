using FluentCineworld.Listings.GetDates;
using FluentCineworld.Listings.GetFilms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public class CineworldListings : ICineworldListings
    {
        private readonly Cinema cinema;
        private readonly IGetDatesQuery getDatesQuery;
        private readonly IFilter filter;
        private readonly IGetFilmsQuery getFilmsQuery;

        public CineworldListings(Cinema cinema, IGetDatesQuery getDatesQuery, IFilter filter, IGetFilmsQuery getFilmsQuery)
        {
            this.cinema = cinema;
            this.getDatesQuery = getDatesQuery;
            this.filter = filter;
            this.getFilmsQuery = getFilmsQuery;
        }

        public ICineworldListings ForDayOfWeek(DayOfWeek dayOfWeek)
        {
            filter.DayOfWeek(dayOfWeek);
            return this;
        }

        public ICineworldListings From(DateTime from)
        {
            filter.From(from);
            return this;
        }

        public ICineworldListings To(DateTime to)
        {
            filter.To(to);
            return this;
        }
       
        public async Task<IEnumerable<Film>> RetrieveAsync()
        {
            var dates = await this.GetDates();
            var films = await this.GetFilms(dates);

            var mergedFilms = this.Merge(films);
            var orderedFilms = mergedFilms.OrderBy(film => film.Name);

            return orderedFilms;
        }

        private async Task<IEnumerable<DateTime>> GetDates()
        {
            var allDates = await this.getDatesQuery.ExecuteAsync(this.cinema);
            var filteredDates = allDates.Where(this.filter.Apply);

            return filteredDates;
        }

        private async Task<IEnumerable<Film>> GetFilms(IEnumerable<DateTime> dates)
        {
            var allTasks = new List<Task<IEnumerable<Film>>>();

            foreach (var date in dates)
            {
                var task = this.getFilmsQuery.ExecuteAsync(this.cinema, date);
                allTasks.Add(task);
            }

            var films = await Task.WhenAll(allTasks);

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