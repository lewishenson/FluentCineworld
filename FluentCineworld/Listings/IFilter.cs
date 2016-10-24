using System;

namespace FluentCineworld.Listings
{
    public interface IFilter
    {
        void DayOfWeek(DayOfWeek dayOfWeek);

        void From(DateTime value);

        void To(DateTime value);

        bool Apply(Film film);
    }
}