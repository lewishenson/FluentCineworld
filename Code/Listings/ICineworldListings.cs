using System;
using System.Collections.Generic;

namespace FluentCineworld.Listings
{
    public interface ICineworldListings
    {
        IEnumerable<Film> Retrieve();

        ICineworldListings ForDayOfWeek(DayOfWeek dayOfWeek);

        ICineworldListings From(DateTime from);

        ICineworldListings To(DateTime to);
    }
}