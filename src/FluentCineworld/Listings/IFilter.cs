using System;

namespace FluentCineworld.Listings
{
    public interface IFilter
    {
        void DayOfWeek(DayOfWeek dayOfWeek);

        void From(DateOnly value);

        void To(DateOnly value);

        bool Apply(DateOnly date);
    }
}