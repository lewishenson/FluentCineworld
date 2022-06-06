using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentCineworld.Listings.GetDates
{
    public interface IGetDatesQuery
    {
        Task<IEnumerable<DateOnly>> ExecuteAsync(Cinema cinema, CancellationToken cancellationToken);
    }
}