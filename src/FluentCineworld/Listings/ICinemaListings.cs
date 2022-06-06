using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentCineworld.Listings
{
    public interface ICinemaListings
    {
        ICinemaListings ForDayOfWeek(DayOfWeek dayOfWeek);

        ICinemaListings From(DateOnly from);

        ICinemaListings To(DateOnly to);

        Task<IEnumerable<Film>> RetrieveAsync(CancellationToken cancellationToken);
    }
}
