using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentCineworld.Listings
{
    public static class ShowingAttributes
    {
        public static readonly ShowingAttribute Universal = new ShowingAttribute("u", "U", ShowingAttributeType.AgeRestriction);
        public static readonly ShowingAttribute ParentalGuidance = new ShowingAttribute("pg", "PG", ShowingAttributeType.AgeRestriction);
        public static readonly ShowingAttribute TwelveA = new ShowingAttribute("12a", "12A", ShowingAttributeType.AgeRestriction);
        public static readonly ShowingAttribute Fifteen = new ShowingAttribute("15", "15", ShowingAttributeType.AgeRestriction);
        public static readonly ShowingAttribute Eighteen = new ShowingAttribute("18", "18", ShowingAttributeType.AgeRestriction);
        public static readonly ShowingAttribute ToBeConfirmed = new ShowingAttribute("tbc", "TBC", ShowingAttributeType.AgeRestriction);

        public static readonly ShowingAttribute AlternativeContent = new ShowingAttribute("alternative-content", "Alternative content", ShowingAttributeType.Extra);
        public static readonly ShowingAttribute AutismFriendly = new ShowingAttribute("autism-friendly", "Autism friendly", ShowingAttributeType.Extra);
        public static readonly ShowingAttribute Cinebabies = new ShowingAttribute("cinebabies", "Cinebabies", ShowingAttributeType.Extra);
        public static readonly ShowingAttribute MoviesForJuniors = new ShowingAttribute("movies-for-juniors", "Movies for Juniors", ShowingAttributeType.Extra);
        public static readonly ShowingAttribute UnlimitedScreening = new ShowingAttribute("unlimited-screening", "Unlimited screening", ShowingAttributeType.Extra);

        public static readonly ShowingAttribute TwoD = new ShowingAttribute("2d", "2D", ShowingAttributeType.ScreeningType);
        public static readonly ShowingAttribute ThreeD = new ShowingAttribute("3d", "3D", ShowingAttributeType.ScreeningType);
        public static readonly ShowingAttribute DolbyAtmos = new ShowingAttribute("dolby-atmos", "Dolby Atmos", ShowingAttributeType.ScreeningType);

        public static readonly ShowingAttribute FourDX = new ShowingAttribute("4dx", "4DX", ShowingAttributeType.SpecialType);
        public static readonly ShowingAttribute Imax = new ShowingAttribute("imax", "IMAX", ShowingAttributeType.SpecialType);
        public static readonly ShowingAttribute Superscreen = new ShowingAttribute("superscreen", "Superscreen", ShowingAttributeType.SpecialType);
        public static readonly ShowingAttribute Vip = new ShowingAttribute("vip", "VIP", ShowingAttributeType.SpecialType);

        public static readonly IEnumerable<ShowingAttribute> All;
        public static readonly IEnumerable<ShowingAttribute> AgeRestrictions;

        static ShowingAttributes()
        {
            All = typeof(ShowingAttributes).GetRuntimeFields()
                                           .Where(field => field.FieldType == typeof(ShowingAttribute))
                                           .Select(field => field.GetValue(null))
                                           .Cast<ShowingAttribute>()
                                           .ToList();

            AgeRestrictions = All.Where(attribute => attribute.AttributeType == ShowingAttributeType.AgeRestriction).ToList();
        }
    }
}