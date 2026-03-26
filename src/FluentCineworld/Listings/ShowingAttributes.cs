using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentCineworld.Listings
{
    public static class ShowingAttributes
    {
        public static readonly ShowingAttribute Universal = new ShowingAttribute(
            "u",
            "U",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute ParentalGuidance = new ShowingAttribute(
            "pg",
            "PG",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute TwelveA = new ShowingAttribute(
            "12a",
            "12A",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute Fifteen = new ShowingAttribute(
            "15",
            "15",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute Eighteen = new ShowingAttribute(
            "18",
            "18",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute ToBeConfirmed = new ShowingAttribute(
            "tbc",
            "TBC",
            ShowingAttributeType.Rating
        );

        public static readonly ShowingAttribute TwoD = new ShowingAttribute(
            "2d",
            "2D",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute ThreeD = new ShowingAttribute(
            "3d",
            "3D",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute FourDX = new ShowingAttribute(
            "4dx",
            "4DX",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute Imax = new ShowingAttribute(
            "imax",
            "IMAX",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute Laser = new ShowingAttribute(
            "laser",
            "Laser",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute ScreenX = new ShowingAttribute(
            "screenx",
            "ScreenX",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute Superscreen = new ShowingAttribute(
            "superscreen",
            "Superscreen",
            ShowingAttributeType.Format
        );

        public static readonly ShowingAttribute AlternativeContent = new ShowingAttribute(
            "alternative-content",
            "Alternative content",
            ShowingAttributeType.Screening
        );
        public static readonly ShowingAttribute Cinebabies = new ShowingAttribute(
            "cinebabies",
            "Cinebabies",
            ShowingAttributeType.Screening
        );
        public static readonly ShowingAttribute MoviesForJuniors = new ShowingAttribute(
            "movies-for-juniors",
            "Movies for Juniors",
            ShowingAttributeType.Screening
        );
        public static readonly ShowingAttribute UnlimitedScreening = new ShowingAttribute(
            "unlimited-screening",
            "Unlimited screening",
            ShowingAttributeType.Screening
        );

        public static readonly ShowingAttribute AudioDescribed = new ShowingAttribute(
            "audio-described",
            "Audio Described",
            ShowingAttributeType.Accessibility
        );
        public static readonly ShowingAttribute AutismFriendly = new ShowingAttribute(
            "autism-friendly",
            "Autism friendly",
            ShowingAttributeType.Accessibility
        );
        public static readonly ShowingAttribute Subbed = new ShowingAttribute(
            "subbed",
            "Subtitled",
            ShowingAttributeType.Accessibility
        );

        public static readonly ShowingAttribute Recliner = new ShowingAttribute(
            "recliner",
            "Recliner",
            ShowingAttributeType.Seating
        );
        public static readonly ShowingAttribute ReservedSelected = new ShowingAttribute(
            "reserved-selected",
            "Reserved Selected",
            ShowingAttributeType.Seating
        );

        public static readonly ShowingAttribute Action = new ShowingAttribute(
            "action",
            "Action",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Adventure = new ShowingAttribute(
            "adventure",
            "Adventure",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Animation = new ShowingAttribute(
            "animation",
            "Animation",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute ClassicFilm = new ShowingAttribute(
            "classicfilm",
            "Classic Film",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Comedy = new ShowingAttribute(
            "comedy",
            "Comedy",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Crime = new ShowingAttribute(
            "crime",
            "Crime",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Documentary = new ShowingAttribute(
            "documentary",
            "Documentary",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Drama = new ShowingAttribute(
            "drama",
            "Drama",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Horror = new ShowingAttribute(
            "horror",
            "Horror",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Suspense = new ShowingAttribute(
            "suspense",
            "Suspense",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Hindi = new ShowingAttribute(
            "hindi",
            "Hindi",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Malayalam = new ShowingAttribute(
            "malayalam",
            "Malayalam",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Tamil = new ShowingAttribute(
            "tamil",
            "Tamil",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Telugu = new ShowingAttribute(
            "telugu",
            "Telugu",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Urdu = new ShowingAttribute(
            "urdu",
            "Urdu",
            ShowingAttributeType.Language
        );

        public static readonly IEnumerable<ShowingAttribute> All;
        public static readonly IEnumerable<ShowingAttribute> AgeRestrictions;

        static ShowingAttributes()
        {
            All = typeof(ShowingAttributes)
                .GetRuntimeFields()
                .Where(field => field.FieldType == typeof(ShowingAttribute))
                .Select(field => field.GetValue(null))
                .Cast<ShowingAttribute>()
                .ToList();

            AgeRestrictions = All.Where(attribute =>
                    attribute.AttributeType == ShowingAttributeType.Rating
                )
                .ToList();
        }
    }
}
