using System;
using System.Collections.Generic;
using System.Linq;
using FluentCineworld.OldListings;

namespace FluentCineworld.UnitTests.Builders
{
    internal class FilmBuilder : IBuilder<Film>
    {
        private readonly ICollection<Func<DayBuilder, Day>> _dayBuilders = new List<Func<DayBuilder, Day>>();

        private string _title;

        private string _rating;

        public FilmBuilder()
        {
            _title = "Test Film";
            _rating = "T";
        }

        public FilmBuilder WithTitle(string title)
        {
            _title = title;

            return this;
        }

        public FilmBuilder WithRating(string rating)
        {
            _rating = rating;

            return this;
        }

        public FilmBuilder WithDay(Func<DayBuilder, Day> build)
        {
            _dayBuilders.Add(build);

            return this;
        }

        public Film Build()
        {
            var film = new Film
                           {
                               Title = _title,
                               Rating = _rating,
                               Days = _dayBuilders.Select(build => build(new DayBuilder()))
                           };

            return film;
        }
    }
}