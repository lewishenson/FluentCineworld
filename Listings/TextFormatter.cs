using System.Threading;

namespace LewisHenson.FluentCineworld.Listings
{
    public class TextFormatter
    {
        public static string FormatTitle(string rawTitle)
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
            else if (formattedTitle.EndsWith(": Unlimited Screening"))
            {
                formattedTitle = formattedTitle.Replace(": Unlimited Screening", string.Empty);
            }
            else if (formattedTitle.EndsWith(": Cineworld Unlimited Exclusive Show"))
            {
                formattedTitle = formattedTitle.Replace(": Cineworld Unlimited Exclusive Show", string.Empty);
            }

            if (formattedTitle.StartsWith("Take 2 Thursday -"))
            {
                formattedTitle = formattedTitle.Replace("Take 2 Thursday - ", string.Empty);
            }
            else if (formattedTitle.StartsWith("Take 2 -"))
            {
                formattedTitle = formattedTitle.Replace("Take 2 - ", string.Empty);
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
}