using System;

namespace FluentCineworld.Utilities
{
    internal static class UriGenerator
    {
        public static string Listings(Cinema cinema)
        {
            return "https://www.cineworld.co.uk/pgm-site?si=" + cinema.Value;
        }

        public static string CinemaSites()
        {
            var oneYearFromNow = DateTime.UtcNow.AddYears(1).ToString("yyyy-MM-dd");

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/cinemas/with-event/until/{oneYearFromNow}?attr=&lang=en_GB";
        }
    }
}