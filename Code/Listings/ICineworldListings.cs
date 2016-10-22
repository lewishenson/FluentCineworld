using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public interface ICineworldListings
    {
        Task<IEnumerable<Film>> RetrieveAsync();

        IEnumerable<Film> Retrieve();

        ICineworldListings ForDayOfWeek(DayOfWeek dayOfWeek);

        ICineworldListings From(DateTime from);

        ICineworldListings To(DateTime to);
    }
}