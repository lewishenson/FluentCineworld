using System;
using System.Collections.Generic;
using System.Linq;
using FluentCineworld.Listings;

namespace FluentCineworld.UnitTests.Builders
{
    internal class FilmBuilder : IBuilder<Film>
    {
        private readonly ICollection<Func<DayBuilder, Day>> dayBuilders = new List<Func<DayBuilder, Day>>();

        private string id;

        private string name;

        private string rating;

        public FilmBuilder()
        {
            this.id = Guid.NewGuid().ToString();
            this.name = "Test Film";
            this.rating = "T";
        }

        public FilmBuilder WithId(string value)
        {
            this.id = value;

            return this;
        }

        public FilmBuilder WithName(string value)
        {
            this.name = value;

            return this;
        }

        public FilmBuilder WithRating(string value)
        {
            this.rating = value;

            return this;
        }

        public FilmBuilder WithDay(Func<DayBuilder, Day> build)
        {
            dayBuilders.Add(build);

            return this;
        }

        public Film Build()
        {
            var film = new Film
                {
                    Id = this.id,
                    Name = this.name,
                    Rating = this.rating,
                    Days = dayBuilders.Select(build => build(new DayBuilder()))
                };

            return film;
        }
    }
}