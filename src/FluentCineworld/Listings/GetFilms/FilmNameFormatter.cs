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

            var result = this.RemoveMoviesForJuniorsText(trimmedName);
            if (result.HasChanged)
            {
                return result.Name;
            }

            result = this.RemoveUnlimitedScreeningText(trimmedName);
            if (result.HasChanged)
            {
                return result.Name;
            }

            result = this.RemoveRereleaseText(trimmedName);
            if (result.HasChanged)
            {
                return result.Name;
            }

            result = this.RemoveAutismFriendlyScreeningText(trimmedName);
            if (result.HasChanged)
            {
                return result.Name;
            }

            result = this.RemoveClassicMoviesText(trimmedName);
            if (result.HasChanged)
            {
                return result.Name;
            }

            return trimmedName;
        }

        private Result RemoveMoviesForJuniorsText(string name)
        {
            if (name.EndsWith(": Subtitled Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(": Subtitled Movies For Juniors", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" - Subtitled Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(" - Subtitled Movies For Juniors", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" Subtitled Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(" Subtitled Movies For Juniors", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(": Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(": Movies For Juniors", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" - Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(" - Movies For Juniors", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            if (name.EndsWith(" Movies For Juniors", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(" Movies For Juniors", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveUnlimitedScreeningText(string name)
        {
            if (name.EndsWith(": Unlimited Screening", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(": Unlimited Screening", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveRereleaseText(string name)
        {
            if (name.EndsWith(" - Rerelease", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace(" - Rerelease", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveAutismFriendlyScreeningText(string name)
        {
            if (name.StartsWith("Autism Friendly Screening: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace("Autism Friendly Screening: ", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private Result RemoveClassicMoviesText(string name)
        {
            if (name.StartsWith("Classic Movies: ", StringComparison.CurrentCultureIgnoreCase))
            {
                var formattedName = name.Replace("Classic Movies: ", string.Empty, StringComparison.CurrentCultureIgnoreCase);
                return Result.AsChanged(formattedName);
            }

            return Result.AsUnchanged(name);
        }

        private class Result
        {
            private Result(string name, bool hasChanged)
            {
                this.Name = name;
                this.HasChanged = hasChanged;
            }

            public string Name { get; }

            public bool HasChanged { get; }

            public static Result AsChanged(string name)
            {
                return new Result(name, true);
            }

            public static Result AsUnchanged(string name)
            {
                return new Result(name, false);
            }
        }
    }
}