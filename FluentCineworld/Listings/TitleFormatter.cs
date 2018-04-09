namespace FluentCineworld.Listings
{
    public class TitleFormatter
    {
        public static string Format(string rawTitle)
        {
            var formattedTitle = rawTitle.Trim();

            formattedTitle = RemoveUnlimitedScreeningSuffix(formattedTitle);
            formattedTitle = RemoveMoviesForJuniorsSuffix(formattedTitle);

            return formattedTitle.Trim();
        }

        private static string RemoveUnlimitedScreeningSuffix(string formattedTitle)
        {
            if (formattedTitle.EndsWith("- Unlimited Screening"))
            {
                formattedTitle = formattedTitle.Replace("- Unlimited Screening", string.Empty);
            }
            else if (formattedTitle.EndsWith(": Unlimited Screening"))
            {
                formattedTitle = formattedTitle.Replace(": Unlimited Screening", string.Empty);
            }
            else if (formattedTitle.EndsWith(": Cineworld Unlimited Exclusive Show"))
            {
                formattedTitle = formattedTitle.Replace(": Cineworld Unlimited Exclusive Show", string.Empty);
            }
            return formattedTitle;
        }

        private static string RemoveMoviesForJuniorsSuffix(string formattedTitle)
        {
            if (formattedTitle.EndsWith("- Movies For Juniors"))
            {
                formattedTitle = formattedTitle.Replace("- Movies For Juniors", string.Empty);
            }
            else if (formattedTitle.EndsWith("- Subtitled Movies For Juniors"))
            {
                formattedTitle = formattedTitle.Replace("- Subtitled Movies For Juniors", string.Empty);
            }
            else if (formattedTitle.EndsWith("Subtitled Movies For Juniors"))
            {
                formattedTitle = formattedTitle.Replace("Subtitled Movies For Juniors", string.Empty);
            }
            else if (formattedTitle.EndsWith("Movies For Juniors"))
            {
                formattedTitle = formattedTitle.Replace("Movies For Juniors", string.Empty);
            }

            return formattedTitle;
        }
    }
}