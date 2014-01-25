using System;
using System.Collections.Generic;

namespace LewisHenson.CineworldCinemas.Listings
{
    public interface ICineworldListings
    {
        ICineworldListings ForDayOfWeek(DayOfWeek dayOfWeek);

        ICineworldListings From(DateTime from);

        ICineworldListings To(DateTime to);

        IEnumerable<Movie> Retrieve();
    }
}
