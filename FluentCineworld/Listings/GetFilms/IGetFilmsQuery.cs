using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentCineworld.Listings.GetFilms
{
    public interface IGetFilmsQuery
    {
        Task<IEnumerable<Film>> ExecuteAsync(Cinema cinema, DateTime date);
    }
}