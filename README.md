# FluentCineworld [![NuGet package](https://buildstats.info/nuget/FluentCineworld)](https://www.nuget.org/packages/FluentCineworld)

Fluent C# API for obtaining Cineworld listings.

The API uses the fluid builder pattern so the below examples can be combined.

## Version 4.x (unreleased, in development)

FluentCineworld uses the new Cineworld Data API Service.

## Version 3.x

FluentCineworld now runs on both .NET Core and the full .NET Framework.

Asynchronous support has also been added.

## Usage

Getting all listings for a cinema:

```csharp
var shows = await Cineworld.WhatsOn(Cinema.MiltonKeynes)
                           .RetrieveAsync();
```

Getting all listings between certain dates:

```csharp
var shows = await Cineworld.WhatsOn(Cinema.MiltonKeynes)
                           .From(new DateTime(2014, 1, 1))
                           .To(new DateTime(2014, 1, 8))
                           .RetrieveAsync();
```

Getting all listings for a certain day of week:

```csharp
var shows = await Cineworld.WhatsOn(Cinema.MiltonKeynes)
                           .ForDayOfWeek(DayOfWeek.Friday)
                           .RetrieveAsync();
```                     


## NuGet

If you don't care about the source code you can just install FluentCineworld using NuGet.

```powershell
PM> Install-Package FluentCineworld
```
