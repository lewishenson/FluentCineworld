namespace FluentCineworld.Listings
{
    public class TitleFormatter
    {
        public static string Format(string rawTitle)
        {
            var formattedTitle = rawTitle.Trim();

            formattedTitle = RemoveUnlimitedScreeningSuffix(formattedTitle);
            formattedTitle = RemoveMoviesForJuniorsSuffix(formattedTitle);
            formattedTitle = RemoveTake2Prefix(formattedTitle);
            formattedTitle = Remove2DOr3DPrefix(formattedTitle);

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
            return formattedTitle;
        }

        private static string RemoveTake2Prefix(string formattedTitle)
        {
            if (formattedTitle.StartsWith("Take 2 Thursday -"))
            {
                formattedTitle = formattedTitle.Replace("Take 2 Thursday - ", string.Empty);
            }
            else if (formattedTitle.StartsWith("Take 2 -"))
            {
                formattedTitle = formattedTitle.Replace("Take 2 - ", string.Empty);
            }
            return formattedTitle;
        }

        private static string Remove2DOr3DPrefix(string formattedTitle)
        {
            if (formattedTitle.StartsWith("2D - "))
            {
                formattedTitle = formattedTitle.Substring(5);
            }
            else if (formattedTitle.StartsWith("3D - "))
            {
                formattedTitle = formattedTitle.Substring(5);
            }
            return formattedTitle;
        }
    }
}