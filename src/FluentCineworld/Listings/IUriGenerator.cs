using System;

namespace FluentCineworld.Listings
{
    public interface IUriGenerator
    {
        string ForDatesWithListings(Cinema cinema);

        string ForListings(Cinema cinema, DateTime date);
    }
}