using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using FluentCineworld.Utilities;

namespace FluentCineworld.OldListings
{
    public class SyndicationListingsScraper : IScraper<IEnumerable<Film>>
    {
        private readonly IWebClient _webClient;

        public SyndicationListingsScraper(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public IEnumerable<Film> Scrape(Cinema cinema)
        {
            var uri = UriGenerator.SyndicationListings();
            var content = _webClient.GetContent(uri);
            if (string.IsNullOrWhiteSpace(content))
            {
                return Enumerable.Empty<Film>();
            }

            var parser = new SyndicationListingsParser();
            var films = parser.Parse(content, cinema.Value);

            var mergedFilms = films.GroupBy(f => f.Title)
                                   .Select(Film.Merge)
                                   .ToList();

            return mergedFilms;
        }

        private class SyndicationListingsParser
        {
            private readonly FilmParser _filmParser = new FilmParser();

            public IEnumerable<Film> Parse(string content, int cinemaId)
            {
                var cinemasElement = XElement.Parse(content);
                var cinemaPath = string.Format("/cinema[@id={0}]", cinemaId);
                var cinemaElement = cinemasElement.XPathSelectElement(cinemaPath);

                var films = new List<Film>();

                if (cinemaElement != null)
                {
                    var filmElements = cinemaElement.Descendants("listing").Descendants("film");
                    films.AddRange(filmElements.Select(filmElement => _filmParser.Parse(filmElement)));
                }

                return films;
            }
        }

        private class FilmParser
        {
            private readonly DaysParser _daysParser = new DaysParser();

            public Film Parse(XElement filmElement)
            {
                var film = new Film
                               {
                                   Title = TextFormatter.FormatTitle(filmElement.Attribute("title").Value),
                                   Rating = filmElement.Attribute("rating").Value,
                                   Days = _daysParser.Parse(filmElement.Descendants("shows").Descendants("show"))
                               };

                return film;
            }
        }

        private class DaysParser
        {
            private readonly ShowParser _showParser = new ShowParser();

            public IEnumerable<Day> Parse(IEnumerable<XElement> showElements)
            {
                var allShows = showElements.Select(showElement => _showParser.Parse(showElement));

                var days = new List<Day>();

                var showsGroupedByDate = allShows.GroupBy(s => s.Time.Date);
                foreach (var date in showsGroupedByDate)
                {
                    var shows = date.GroupBy(s => s.Time).Select(Show.Merge);
                    var day = new Day(date.Key, shows);
                    days.Add(day);
                }

                return days;
            }
        }

        private class ShowParser
        {
            public Show Parse(XElement showElement)
            {
                var show = new Show();

                var rawTime = showElement.Attribute("time").Value;
                show.Time = DateTime.Parse(rawTime);

                var videoTypeAttribute = showElement.Attribute("videoType");
                if (videoTypeAttribute != null)
                {
                    show.Imax = videoTypeAttribute.Value.Contains("imax");
                    show.Is3D = videoTypeAttribute.Value.Contains("3d");
                    show.Is2D = !show.Is3D;
                }
                else
                {
                    show.Is2D = true;
                }

                var sessionTypeAttribute = showElement.Attribute("sessionType");
                if (sessionTypeAttribute != null)
                {
                    show.DBox = sessionTypeAttribute.Value.Contains("dbox");
                    show.Vip = sessionTypeAttribute.Value.Contains("vip");
                    show.Is4Dx = sessionTypeAttribute.Value.Contains("4dx");
                    show.Superscreen = sessionTypeAttribute.Value.Contains("sr");
                }

                var audioTypeAttribute = showElement.Attribute("audioType");
                if (audioTypeAttribute != null)
                {
                    show.AudioDescribed = audioTypeAttribute.Value.Contains("audio described");
                }

                var subtitledAttribute = showElement.Attribute("subtitled");
                show.Subtitled = subtitledAttribute != null;

                return show;
            }
        }
    }
}