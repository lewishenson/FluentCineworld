using System;

namespace FluentCineworld.Listings.GetFilms
{
    public class FilmNameFormatter : IFilmNameFormatter
    {
        public string Format(string originalName)
        {
            var trimmedName = originalName?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(originalName))
            {
                return string.Empty;
            }

            var mutatedName = RemovePrefix(trimmedName);

            return RemoveSuffix(mutatedName);
        }

        private string RemovePrefix(string name)
        {
            var result = RemoveScreeningTypePrefixText(name);

            result = RemoveSeasonText(result.Name);

            result = RemoveFamilyFilmsText(result.Name);

            result = RemoveMoviesForJuniorsPrefixText(result.Name);

            result = RemoveAutismFriendlyScreeningText(result.Name);

            result = RemoveClassicMoviesText(result.Name);

            return result.Name;
        }

        private Result RemoveSeasonText(string name)
        {
            if (name.StartsWith("Sci-Fi Season: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "Sci-Fi Season: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("X-Mas Season: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "X-Mas Season: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("Awards Season: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "Awards Season: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("Music Icons Season: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "Music Icons Season: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("Baz Luhrmann Season ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "Baz Luhrmann Season ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveScreeningTypePrefixText(string name)
        {
            if (name.StartsWith("(2D) ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "(2D) ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("(4DX) ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "(4DX) ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("(ScreenX) ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "(ScreenX) ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("(SS) ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "(SS) ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("(IMAX) ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "(IMAX) ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveFamilyFilmsText(string name)
        {
            if (name.StartsWith("£2 Family Films : ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "£2 Family Films : ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveMoviesForJuniorsPrefixText(string name)
        {
            if (name.StartsWith("M4J : ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "M4J : ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("M4J: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "M4J: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.StartsWith("(M4J) ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "(M4J) ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveAutismFriendlyScreeningText(string name)
        {
            if (
                name.StartsWith(
                    "Autism Friendly Screening: ",
                    StringComparison.CurrentCultureIgnoreCase
                )
            )
            {
                var formattedName = name.Replace(
                    "Autism Friendly Screening: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (
                name.StartsWith(
                    "Autism Friendly Screening : ",
                    StringComparison.CurrentCultureIgnoreCase
                )
            )
            {
                var formattedName = name.Replace(
                    "Autism Friendly Screening : ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveClassicMoviesText(string name)
        {
            if (name.StartsWith("Classic Movies: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    "Classic Movies: ",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private string RemoveSuffix(string name)
        {
            var result = RemoveScreeningTypeSuffixText(name);

            result = RemoveMoviesForJuniorsSuffixText(result.Name);

            result = RemoveUnlimitedScreeningText(result.Name);

            result = RemoveRereleaseText(result.Name);

            return result.Name;
        }

        private Result RemoveScreeningTypeSuffixText(string name)
        {
            if (name.EndsWith(" (Subtitled)", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " (Subtitled)",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveMoviesForJuniorsSuffixText(string name)
        {
            if (
                name.EndsWith(
                    ": Subtitled Movies For Juniors",
                    StringComparison.CurrentCultureIgnoreCase
                )
            )
            {
                var formattedName = name.Replace(
                    ": Subtitled Movies For Juniors",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (
                name.EndsWith(
                    " - Subtitled Movies For Juniors",
                    StringComparison.CurrentCultureIgnoreCase
                )
            )
            {
                var formattedName = name.Replace(
                    " - Subtitled Movies For Juniors",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (
                name.EndsWith(
                    " Subtitled Movies For Juniors",
                    StringComparison.CurrentCultureIgnoreCase
                )
            )
            {
                var formattedName = name.Replace(
                    " Subtitled Movies For Juniors",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(": Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    ": Movies For Juniors",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" - Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " - Movies For Juniors",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " Movies For Juniors",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveUnlimitedScreeningText(string name)
        {
            if (name.EndsWith(": Unlimited Screening", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    ": Unlimited Screening",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" Unlimited Screening", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " Unlimited Screening",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" Unlimited Scr", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " Unlimited Scr",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveRereleaseText(string name)
        {
            if (name.EndsWith(" - Rerelease", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " - Rerelease",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" (10th Anniversary)", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " (10th Anniversary)",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" (25th Anniversary)", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(
                    " (25th Anniversary)",
                    string.Empty,
                    StringComparison.CurrentCultureIgnoreCase
                );
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private record Result
        {
            private Result(string name, bool hasChanged)
            {
                Name = name;
                HasChanged = hasChanged;
            }

            public string Name { get; }

            public bool HasChanged { get; }

            public static Result AsChanged(string name) => new(name, true);

            public static Result AsUnchanged(string name) => new(name, false);
        }
    }
}
