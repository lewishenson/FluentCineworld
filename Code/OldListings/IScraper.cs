namespace FluentCineworld.OldListings
{
    public interface IScraper<out T>
    {
        T Scrape(Cinema cinema);
    }
}