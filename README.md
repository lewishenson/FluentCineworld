# FluentCineworld

![Build Status](https://github.com/lewishenson/FluentCineworld/actions/workflows/build-and-test.yml/badge.svg) [![NuGet package](https://buildstats.info/nuget/FluentCineworld)](https://www.nuget.org/packages/FluentCineworld)

Fluent C# API for obtaining Cineworld listings.

The API uses the fluid builder pattern so the below examples can be combined.

## Version 8.x
FluentCineworld 8.x was released as Cineworld closed two cinemas, resulting in a breaking change to the API.

## Version 7.x
FluentCineworld 7.x targets .NET 6 as .NET 5 has reached end of support.

The API itself has changed slightly - `DateOnly` is now used where appropriate.

## Version 6.x
FluentCineworld 6.x targets .NET 5 as it replaces .NET Standard.

The API itself remains mostly untouched - cinema ids are now a string rather than an integer.

## Version 5.x
FluentCineworld 5.x targets .NET Standard 2.0 and therefore can be run on either .NET Core and the full .NET Framework.

Version 5 is very similiar to version 4 but is easier to use with Dependency Injection.

## Version 4.x

FluentCineworld 4.x targets .NET Standard 1.2 and therefore can be run on either .NET Core and the full .NET Framework.

Version 4 uses the new Cineworld Data API Service and therefore the returned data has changed from previous releases.

## Version 3.x

Version 3 will no longer return data as Cineworld have changed their site.

## Usage

To obtain listings, an ICineworld instance is required.
The class Cineworld implements this interface and only requires an HttpClient instance.
This can be come from FluentCineworld's Shared class (as shown below), provided via a HttpClientFactory or any other source.

Getting all listings for a cinema:

```csharp
var cineworld = new Cineworld(Shared.HttpClient);
var shows = await cineworld.WhatsOn(Cinema.MiltonKeynes)
                           .RetrieveAsync();
```

Getting all listings between certain dates:

```csharp
var cineworld = new Cineworld(Shared.HttpClient);
var shows = await Cineworld.WhatsOn(Cinema.MiltonKeynes)
                           .From(new DateTime(2018, 1, 1))
                           .To(new DateTime(2018, 1, 8))
                           .RetrieveAsync();
```

Getting all listings for a certain day of week:

```csharp
var cineworld = new Cineworld(Shared.HttpClient);
var shows = await cineworld.WhatsOn(Cinema.MiltonKeynes)
                           .ForDayOfWeek(DayOfWeek.Friday)
                           .RetrieveAsync();
```


## NuGet

If you don't care about the source code you can just install FluentCineworld using NuGet.

```powershell
PM> Install-Package FluentCineworld
```
