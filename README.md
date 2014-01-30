FluentCineworld
===============

Fluent C# API for obtaining Cineworld listings.

Two methods are supported - using the Cineworld syndication feed (default behaviour) or scraping the Cineworld site.

The API uses the fluid builder pattern so the below examples can be combined.


Usage
=====

Getting all listings for a cinema:

var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .Retrieve();


Getting all listings between certain dates:

var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .From(new DateTime(2014, 1, 1))
                     .To(new DateTime(2014, 1, 8))
                     .Retrieve();


Getting all listings for a certain day of week:

var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .ForDayOfWeek(DayOfWeek.Friday)
                     .Retrieve();
                     

Using the alternative behaviour of scraping the Cineworld site to obtain the data:

var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .UsingSyndication(false)
                     .Retrieve();
