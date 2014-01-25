namespace LewisHenson.CineworldCinemas.Listings
{
    public interface IScraper<out T>
    {
        T Scrape(string url);
    }
}