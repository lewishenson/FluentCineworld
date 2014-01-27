using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace LewisHenson.CineworldCinemas.Listings
{
    internal class SiteListingsScraper : IScraper<IEnumerable<Film>>
    {
        public IEnumerable<Film> Scrape(Cinema cinema)
        {
            var uri = UriGenerator.WhatsOn(cinema);
            var content = WebRequestHelper.GetContent(uri);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<Film>();
            }

            var parser = new WhatsOnParser();
            var films = parser.Parse(content);

            return films;
        }

        private class WhatsOnParser
        {
            private readonly FilmParser _filmParser = new FilmParser();

            public IEnumerable<Film> Parse(string content)
            {
                var filmsStart = content.IndexOf("<div id=\"filter-reload\" class=\"row\">", StringComparison.InvariantCultureIgnoreCase);
                var filmsEnd = content.IndexOf("<div class=\"filter\">", filmsStart, StringComparison.InvariantCultureIgnoreCase);

                var filmsSubString = content.Substring(filmsStart, filmsEnd - filmsStart);
                var filmSubStrings = filmsSubString.Split(new[] { "<div class=\"poster\">" }, StringSplitOptions.RemoveEmptyEntries);

                var films = new List<Film>();

                foreach (var filmSubString in filmSubStrings)
                {
                    var film = _filmParser.Parse(filmSubString);
                    if (film != null)
                    {
                        films.Add(film);
                    }
                }

                return films;
            }
        }

        private class FilmParser
        {
            private readonly DaysParser _daysParser = new DaysParser();

            public Film Parse(string content)
            {
                if (!content.Contains("<h1>"))
                {
                    return null;
                }

                var film = new Film();

                try
                {
                    var filmTitleStart = content.IndexOf("<h1>", StringComparison.InvariantCultureIgnoreCase) + 4;
                    filmTitleStart = content.IndexOf(">", filmTitleStart, StringComparison.InvariantCultureIgnoreCase) + 1;

                    var filmTitleEnd = content.IndexOf("<", filmTitleStart, StringComparison.InvariantCultureIgnoreCase);

                    var rawFilmTitle = content.Substring(filmTitleStart, filmTitleEnd - filmTitleStart);
                    film.Title = TextFormatter.FormatTitle(rawFilmTitle.Trim());

                    if (string.IsNullOrWhiteSpace(film.Title))
                    {
                        return null;
                    }

                    var certificationStart = content.IndexOf("<a class=\"classification", filmTitleEnd, StringComparison.InvariantCultureIgnoreCase);
                    if (certificationStart > 0)
                    {
                        certificationStart = content.IndexOf(">", certificationStart, StringComparison.InvariantCultureIgnoreCase) + 2;
                        var certificationEnd = content.IndexOf("<", certificationStart, StringComparison.InvariantCultureIgnoreCase) - 1;
                        film.Rating = content.Substring(certificationStart, certificationEnd - certificationStart).Trim();
                    }
                    else
                    {
                        film.Rating = "?";
                        certificationStart = filmTitleEnd;
                    }

                    film.Data = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

                    var runningTimeStart = content.IndexOf("<h3>Running time:</h3>", certificationStart, StringComparison.InvariantCultureIgnoreCase);
                    if (runningTimeStart > 0)
                    {
                        runningTimeStart = content.IndexOf("<p>", runningTimeStart, StringComparison.InvariantCultureIgnoreCase) + 3;
                        var runningTimeEnd = content.IndexOf("</p>", runningTimeStart, StringComparison.InvariantCultureIgnoreCase);
                        film.Data["RunningTime"] = content.Substring(runningTimeStart, runningTimeEnd - runningTimeStart).Trim();
                    }
                    else
                    {
                        runningTimeStart = certificationStart;
                    }

                    var synopsisStart = content.IndexOf("<h3>Synopsis:</h3>", runningTimeStart, StringComparison.InvariantCultureIgnoreCase);
                    if (synopsisStart > 0)
                    {
                        synopsisStart = content.IndexOf("<p>", synopsisStart, StringComparison.InvariantCultureIgnoreCase) + 3;
                        var synopsisEnd = content.IndexOf("</p>", synopsisStart, StringComparison.InvariantCultureIgnoreCase);
                        film.Data["Synopsis"] = content.Substring(synopsisStart, synopsisEnd - synopsisStart).Trim();
                    }
                    else
                    {
                        synopsisStart = runningTimeStart;
                    }

                    var daysSubstring = content.Substring(synopsisStart);
                    var days = _daysParser.Parse(daysSubstring);
                    if (days != null && days.Any())
                    {
                        film.Days = new List<Day>(days);
                    }
                    else
                    {
                        film = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.ToString());
                    film = null;
                }

                return film;
            }
        }

        private class DaysParser
        {
            private readonly DateParser _dateParser = new DateParser();

            public IList<Day> Parse(string content)
            {
                IList<Day> days = new List<Day>();

                try
                {
                    var daySubStrings = content.Split(new[] { "<h3>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var daySubString in daySubStrings)
                    {
                        var dayEnd = daySubString.IndexOf("</h3>", StringComparison.InvariantCultureIgnoreCase);
                        if (dayEnd < 0)
                        {
                            continue;
                        }

                        var dateString = daySubString.Substring(0, dayEnd);

                        var timesSubStringStart = daySubString.IndexOf("<ol", dayEnd, StringComparison.InvariantCultureIgnoreCase);
                        if (timesSubStringStart < 0)
                        {
                            continue;
                        }

                        var timesSubStringEnd = daySubString.IndexOf("</ol>", timesSubStringStart, StringComparison.InvariantCultureIgnoreCase);
                        var timesSubString = daySubString.Substring(timesSubStringStart, timesSubStringEnd - timesSubStringStart);

                        var showings = new List<Show>();
                        var date = _dateParser.GetDate(dateString);

                        var timeSubStrings = timesSubString.Split(new[] { "<li>" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var timeSubString in timeSubStrings)
                        {
                            var timeStart = timeSubString.IndexOf(">", StringComparison.InvariantCultureIgnoreCase) + 1;
                            var timeEnd = timeSubString.IndexOf("<", timeStart, StringComparison.InvariantCultureIgnoreCase);
                            if (timeEnd < 0)
                            {
                                continue;
                            }

                            var time = timeSubString.Substring(timeStart, timeEnd - timeStart);
                            var timeSpan = TimeSpan.Parse(time);

                            var showing = new Show
                                {
                                    Time = date.Add(timeSpan)
                                };

                            var remainingText = timeSubString.Substring(timeEnd);
                            showing.Is2D = remainingText.Contains("icon-service-2d");
                            showing.Is3D = remainingText.Contains("icon-service-3d");
                            showing.DBox = remainingText.Contains("icon-service-dx");

                            showings.Add(showing);
                        }

                        if (showings.Any())
                        {
                            var day = new Day
                                {
                                    Date = date,
                                    Shows = showings
                                };

                            days.Add(day);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.ToString());
                    days = null;
                }

                return days;
            }
        }

        private class DateParser
        {
            private readonly CultureInfo _britishCulture = new CultureInfo("en-GB");

            public DateTime GetDate(string input)
            {
                DateTime day;

                if (Parse(input, "dddd d MMM", out day))
                {
                    return day;
                }

                var nextYearInput = input + " " + (DateTime.UtcNow.Year + 1);
                if (Parse(nextYearInput, "dddd d MMM yyyy", out day))
                {
                    return day;
                }

                var message = string.Format("Invalid date: {0}", input);
                throw new FormatException(message);
            }

            private bool Parse(string input, string format, out DateTime date)
            {
                var processedInput = input.Replace("st ", " ").Replace("nd ", " ").Replace("rd ", " ").Replace("th ", " ");
                return DateTime.TryParseExact(processedInput, format, _britishCulture, DateTimeStyles.AllowWhiteSpaces, out date);
            }
        }
    }
}