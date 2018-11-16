using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public interface ICinemaListings
    {
        ICinemaListings ForDayOfWeek(DayOfWeek dayOfWeek);

        ICinemaListings From(DateTime from);

        ICinemaListings To(DateTime to);

        Task<IEnumerable<Film>> RetrieveAsync();
    }
}