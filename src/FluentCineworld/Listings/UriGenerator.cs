using System;

namespace FluentCineworld.Listings
{
    public class UriGenerator : IUriGenerator
    {
        private const string DateInUriFormat = "yyyy-MM-dd";

        public string ForDatesWithListings(Cinema cinema)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var oneYearFromNow = this.GetOneYearFromNow();

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/dates/in-cinema/{cinema.Id}/until/{oneYearFromNow}?attr=&lang=en_GB";
        }

        private string GetOneYearFromNow()
        {
            return SystemDate.UtcNow().AddYears(1).ToString(DateInUriFormat);
        }

        public string ForListings(Cinema cinema, DateTime date)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            var formattedDate = date.ToString(DateInUriFormat);

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/film-events/in-cinema/{cinema.Id}/at-date/{formattedDate}?attr=&lang=en_GB";
        }
    }
}