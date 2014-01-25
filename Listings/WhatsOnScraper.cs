using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace LewisHenson.CineworldCinemas.Listings
{
    internal class WhatsOnScraper : IScraper<IEnumerable<Movie>>
    {
        public IEnumerable<Movie> Scrape(string url)
        {
            var content = WebRequestHelper.GetContent(url);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<Movie>();
            }

            var parser = new WhatsOnParser();
            var movies = parser.Parse(content);

            return movies;
        }

        private class WhatsOnParser
        {
            private readonly MovieParser _movieParser = new MovieParser();

            public IEnumerable<Movie> Parse(string content)
            {
                var moviesStart = content.IndexOf("<div id=\"filter-reload\" class=\"row\">", StringComparison.InvariantCultureIgnoreCase);
                var moviesEnd = content.IndexOf("<div class=\"filter\">", moviesStart, StringComparison.InvariantCultureIgnoreCase);

                var moviesSubString = content.Substring(moviesStart, moviesEnd - moviesStart);
                var movieSubStrings = moviesSubString.Split(new[] { "<div class=\"poster\">" }, StringSplitOptions.RemoveEmptyEntries);

                var movies = new List<Movie>();

                foreach (var movieSubString in movieSubStrings)
                {
                    var movie = _movieParser.Parse(movieSubString);
                    if (movie != null)
                    {
                        movies.Add(movie);
                    }
                }

                return movies;
            }
        }

        private class MovieParser
        {
            private readonly DaysParser _daysParser = new DaysParser();

            public Movie Parse(string content)
            {
                if (!content.Contains("<h1>"))
                {
                    return null;
                }

                var movie = new Movie();

                try
                {
                    var movieNameStart = content.IndexOf("<h1>", StringComparison.InvariantCultureIgnoreCase) + 4;
                    movieNameStart = content.IndexOf(">", movieNameStart, StringComparison.InvariantCultureIgnoreCase) + 1;

                    var movieNameEnd = content.IndexOf("<", movieNameStart, StringComparison.InvariantCultureIgnoreCase);

                    var rawMovieName = content.Substring(movieNameStart, movieNameEnd - movieNameStart);
                    movie.Name = FormatTitle(rawMovieName.Trim());

                    if (string.IsNullOrWhiteSpace(movie.Name))
                    {
                        return null;
                    }

                    var certificationStart = content.IndexOf("<a class=\"classification", movieNameEnd, StringComparison.InvariantCultureIgnoreCase);
                    if (certificationStart > 0)
                    {
                        certificationStart = content.IndexOf(">", certificationStart, StringComparison.InvariantCultureIgnoreCase) + 2;
                        var certificationEnd = content.IndexOf("<", certificationStart, StringComparison.InvariantCultureIgnoreCase) - 1;
                        movie.Certificate = content.Substring(certificationStart, certificationEnd - certificationStart).Trim();
                    }
                    else
                    {
                        movie.Certificate = "?";
                        certificationStart = movieNameEnd;
                    }

                    var runningTimeStart = content.IndexOf("<h3>Running time:</h3>", certificationStart, StringComparison.InvariantCultureIgnoreCase);
                    if (runningTimeStart > 0)
                    {
                        runningTimeStart = content.IndexOf("<p>", runningTimeStart, StringComparison.InvariantCultureIgnoreCase) + 3;
                        var runningTimeEnd = content.IndexOf("</p>", runningTimeStart, StringComparison.InvariantCultureIgnoreCase);
                        movie.RunningTime = content.Substring(runningTimeStart, runningTimeEnd - runningTimeStart).Trim();
                    }
                    else
                    {
                        movie.RunningTime = "(unavailable)";
                        runningTimeStart = certificationStart;
                    }

                    var synopsisStart = content.IndexOf("<h3>Synopsis:</h3>", runningTimeStart, StringComparison.InvariantCultureIgnoreCase);
                    if (synopsisStart > 0)
                    {
                        synopsisStart = content.IndexOf("<p>", synopsisStart, StringComparison.InvariantCultureIgnoreCase) + 3;
                        var synopsisEnd = content.IndexOf("</p>", synopsisStart, StringComparison.InvariantCultureIgnoreCase);
                        movie.Synopsis = content.Substring(synopsisStart, synopsisEnd - synopsisStart).Trim();
                    }
                    else
                    {
                        movie.Synopsis = "?";
                        synopsisStart = runningTimeStart;
                    }

                    var daysSubstring = content.Substring(synopsisStart);
                    var days = _daysParser.Parse(daysSubstring);
                    if (days != null && days.Any())
                    {
                        movie.Days = new List<Day>(days);
                    }
                    else
                    {
                        movie = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.ToString());
                    movie = null;
                }

                return movie;
            }

            private static string FormatTitle(string rawTitle)
            {
                var formattedTitle = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(rawTitle.Trim().ToLower());

                if (formattedTitle.Contains("&Amp;"))
                {
                    formattedTitle = formattedTitle.Replace("&Amp;", "&");
                }

                if (formattedTitle.EndsWith("Ii"))
                {
                    formattedTitle = formattedTitle.Replace("Ii", "II");
                }
                else if (formattedTitle.EndsWith("Iii"))
                {
                    formattedTitle = formattedTitle.Replace("Iii", "III");
                }
                else if (formattedTitle.EndsWith("Iv"))
                {
                    formattedTitle = formattedTitle.Replace("Iv", "IV");
                }
                else if (formattedTitle.EndsWith("- Unlimited Screening"))
                {
                    formattedTitle = formattedTitle.Replace("- Unlimited Screening", string.Empty);
                }
                else if (formattedTitle.EndsWith(": Cineworld Unlimited Exclusive Show"))
                {
                    formattedTitle = formattedTitle.Replace(": Cineworld Unlimited Exclusive Show", string.Empty);
                }

                if (formattedTitle.StartsWith("Take 2 Thursday -"))
                {
                    formattedTitle = formattedTitle.Replace("Take 2 Thursday - ", string.Empty);
                }

                if (formattedTitle.StartsWith("2D - "))
                {
                    formattedTitle = formattedTitle.Substring(5) + " [2D]";
                }
                else if (formattedTitle.StartsWith("3D - "))
                {
                    formattedTitle = formattedTitle.Substring(5) + " [3D]";
                }

                return formattedTitle.Trim();
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

                        var timesSubStringStart = daySubString.IndexOf("<ol", dayEnd, StringComparison.InvariantCultureIgnoreCase);
                        if (timesSubStringStart < 0)
                        {
                            continue;
                        }

                        var timesSubStringEnd = daySubString.IndexOf("</ol>", timesSubStringStart, StringComparison.InvariantCultureIgnoreCase);
                        var timesSubString = daySubString.Substring(timesSubStringStart, timesSubStringEnd - timesSubStringStart);

                        var showings = new List<Showing>();

                        var timeSubStrings = timesSubString.Split(new[] { "<li>" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var timeSubString in timeSubStrings)
                        {
                            var timeStart = timeSubString.IndexOf(">", StringComparison.InvariantCultureIgnoreCase) + 1;
                            var timeEnd = timeSubString.IndexOf("<", timeStart, StringComparison.InvariantCultureIgnoreCase);
                            if (timeEnd < 0)
                            {
                                continue;
                            }

                            var showing = new Showing
                                {
                                    Time = timeSubString.Substring(timeStart, timeEnd - timeStart)
                                };

                            var remainingText = timeSubString.Substring(timeEnd);
                            showing.Is2D = remainingText.Contains("icon-service-2d");
                            showing.Is3D = remainingText.Contains("icon-service-3d");
                            showing.IsDBox = remainingText.Contains("icon-service-dx");

                            showings.Add(showing);
                        }

                        if (showings.Any())
                        {
                            var dateString = daySubString.Substring(0, dayEnd);

                            var day = new Day
                                {
                                    Date = _dateParser.GetDate(dateString),
                                    Showings = showings
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