# FluentCineworld [![Build status](https://ci.appveyor.com/api/projects/status/fjx5sr56489k0323)](https://ci.appveyor.com/project/lewishenson/fluentcineworld)

Fluent C# API for obtaining Cineworld listings.

Two approaches are supported - using the Cineworld syndication feed (default behaviour) or scraping the Cineworld site.

The API uses the fluid builder pattern so the below examples can be combined.

## Usage

Getting all listings for a cinema:

```csharp
var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .Retrieve();
```

Getting all listings between certain dates:

```csharp
var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .From(new DateTime(2014, 1, 1))
                     .To(new DateTime(2014, 1, 8))
                     .Retrieve();
```

Getting all listings for a certain day of week:

```csharp
var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .ForDayOfWeek(DayOfWeek.Friday)
                     .Retrieve();
```                     

Using the alternative behaviour of scraping the Cineworld site to obtain the data:

```csharp
var shows = Cineworld.WhatsOn(Cinema.MiltonKeynes)
                     .UsingSyndication(false)
                     .Retrieve();
```


## NuGet

If you don't care about the source code you can just install FluentCineworld using NuGet.

    PM> Install-Package FluentCineworld
