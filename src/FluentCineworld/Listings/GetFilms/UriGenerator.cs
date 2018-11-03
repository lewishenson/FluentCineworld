using System;

namespace FluentCineworld.Listings.GetFilms
{
    public static class UriGenerator
    {
        private const string DateInUriFormat = "yyyy-MM-dd";

        public static string Listings(Cinema cinema, DateTime date)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var formattedDate = date.ToString(DateInUriFormat);

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/film-events/in-cinema/{cinema.Value}/at-date/{formattedDate}?attr=&lang=en_GB";
        }
    }
}