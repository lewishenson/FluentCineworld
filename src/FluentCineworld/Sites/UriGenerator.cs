namespace FluentCineworld.Sites
{
    public class UriGenerator : IUriGenerator
    {
        private const string DateInUriFormat = "yyyy-MM-dd";

        public string ForCinemaSites()
        {
            var oneYearFromNow = this.GetOneYearFromNow();

            return $"https://www.cineworld.co.uk/uk/data-api-service/v1/quickbook/10108/cinemas/with-event/until/{oneYearFromNow}?attr=&lang=en_GB";
        }

        private string GetOneYearFromNow()
        {
            return SystemDate.UtcNow().AddYears(1).ToString(DateInUriFormat);
        }
    }
}
