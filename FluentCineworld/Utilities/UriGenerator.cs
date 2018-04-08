using System;

namespace FluentCineworld.Utilities
{
    internal static class UriGenerator
    {
        private const string DateInUriFormat = "yyyy-MM-dd";

        public static string Listings(Cinema cinema, DateTime date)
        {
            var formattedDate = date.ToString(DateInUriFormat);

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/film-events/in-cinema/{cinema.Value}/at-date/{formattedDate}?attr=&lang=en_GB";
        }

        public static string DatesWithListings(Cinema cinema)
        {
            var oneYearFromNow = GetOneYearFromNow();

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/dates/in-cinema/{cinema.Value}/until/{oneYearFromNow}?attr=&lang=en_GB";
        }

        public static string CinemaSites()
        {
            var oneYearFromNow = GetOneYearFromNow();

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/cinemas/with-event/until/{oneYearFromNow}?attr=&lang=en_GB";
        }

        private static string GetOneYearFromNow()
        {
            return DateTime.UtcNow.AddYears(1).ToString(DateInUriFormat);
        }
    }
}