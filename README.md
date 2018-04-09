# FluentCineworld [![NuGet package](https://buildstats.info/nuget/FluentCineworld)](https://www.nuget.org/packages/FluentCineworld)

Fluent C# API for obtaining Cineworld listings.

The API uses the fluid builder pattern so the below examples can be combined.

## Version 4.x

FluentCineworld targets .NET Standard 1.2 and therefore can be run on either .NET Core and the full .NET Framework.

Version 4 uses the new Cineworld Data API Service and therefore the returned data has changed from previous releases.

## Version 3.x

Version 3 will no longer return data as Cineworld have changed their site.

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
