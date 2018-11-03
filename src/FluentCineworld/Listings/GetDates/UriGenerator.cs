using System;

namespace FluentCineworld.Listings.GetDates
{
    public static class UriGenerator
    {
        private const string DateInUriFormat = "yyyy-MM-dd";

        public static string DatesWithListings(Cinema cinema)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var oneYearFromNow = GetOneYearFromNow();

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/dates/in-cinema/{cinema.Value}/until/{oneYearFromNow}?attr=&lang=en_GB";
        }

        private static string GetOneYearFromNow()
        {
            return SystemDate.UtcNow().AddYears(1).ToString(DateInUriFormat);
        }
    }
}