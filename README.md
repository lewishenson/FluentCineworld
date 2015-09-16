# FluentCineworld [![Build status](http://img.shields.io/appveyor/ci/lewishenson/fluentcineworld.svg?style=flat)](https://ci.appveyor.com/project/lewishenson/FluentCineworld) [![NuGet version](http://img.shields.io/nuget/v/FluentCineworld.svg?style=flat)](https://www.nuget.org/packages/FluentCineworld/)  [![NuGet downloads](http://img.shields.io/nuget/dt/FluentCineworld.svg?style=flat)](https://www.nuget.org/packages/FluentCineworld/)

Fluent C# API for obtaining Cineworld listings.

The API uses the fluid builder pattern so the below examples can be combined.

## Version 2.x

FluentCineworld has been changed to use the JSON data from the Cineworld site now, the HTML scraping and XML parsing approach has been thrown away. Version 2 is faster than before and features some different information - for example, the screen number. Unfortunately there are also a few API breaking changes but hopefully it is worth the pain.

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


## NuGet

If you don't care about the source code you can just install FluentCineworld using NuGet.

    PM> Install-Package FluentCineworld
