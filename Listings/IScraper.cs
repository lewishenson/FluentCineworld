﻿namespace FluentCineworld.Listings
{
    public interface IScraper<out T>
    {
        T Scrape(Cinema cinema);
    }
}