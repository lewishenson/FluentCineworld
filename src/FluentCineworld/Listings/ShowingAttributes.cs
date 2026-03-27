using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentCineworld.Listings
{
    public static class ShowingAttributes
    {
        public static readonly ShowingAttribute Universal = new(
            "u",
            "U",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute ParentalGuidance = new(
            "pg",
            "PG",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute TwelveA = new(
            "12a",
            "12A",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute Fifteen = new(
            "15",
            "15",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute Eighteen = new(
            "18",
            "18",
            ShowingAttributeType.Rating
        );
        public static readonly ShowingAttribute ToBeConfirmed = new(
            "tbc",
            "TBC",
            ShowingAttributeType.Rating
        );

        public static readonly ShowingAttribute TwoD = new("2d", "2D", ShowingAttributeType.Format);
        public static readonly ShowingAttribute ThreeD = new(
            "3d",
            "3D",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute FourDX = new(
            "4dx",
            "4DX",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute Imax = new(
            "imax",
            "IMAX",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute Laser = new(
            "laser",
            "Laser",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute ScreenX = new(
            "screenx",
            "ScreenX",
            ShowingAttributeType.Format
        );
        public static readonly ShowingAttribute Superscreen = new(
            "superscreen",
            "Superscreen",
            ShowingAttributeType.Format
        );

        public static readonly ShowingAttribute AlternativeContent = new(
            "alternative-content",
            "Alternative content",
            ShowingAttributeType.Screening
        );
        public static readonly ShowingAttribute Cinebabies = new(
            "cinebabies",
            "Cinebabies",
            ShowingAttributeType.Screening
        );
        public static readonly ShowingAttribute MoviesForJuniors = new(
            "movies-for-juniors",
            "Movies for Juniors",
            ShowingAttributeType.Screening
        );
        public static readonly ShowingAttribute UnlimitedScreening = new(
            "unlimited-screening",
            "Unlimited screening",
            ShowingAttributeType.Screening
        );

        public static readonly ShowingAttribute AudioDescribed = new(
            "audio-described",
            "Audio Described",
            ShowingAttributeType.Accessibility
        );
        public static readonly ShowingAttribute AutismFriendly = new(
            "autism-friendly",
            "Autism friendly",
            ShowingAttributeType.Accessibility
        );
        public static readonly ShowingAttribute Subbed = new(
            "subbed",
            "Subtitled",
            ShowingAttributeType.Accessibility
        );

        public static readonly ShowingAttribute Recliner = new(
            "recliner",
            "Recliner",
            ShowingAttributeType.Seating
        );
        public static readonly ShowingAttribute ReservedSelected = new(
            "reserved-selected",
            "Reserved Selected",
            ShowingAttributeType.Seating
        );

        public static readonly ShowingAttribute Action = new(
            "action",
            "Action",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Adventure = new(
            "adventure",
            "Adventure",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Animation = new(
            "animation",
            "Animation",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute ClassicFilm = new(
            "classicfilm",
            "Classic Film",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Comedy = new(
            "comedy",
            "Comedy",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Crime = new(
            "crime",
            "Crime",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Documentary = new(
            "documentary",
            "Documentary",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Drama = new(
            "drama",
            "Drama",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Horror = new(
            "horror",
            "Horror",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Suspense = new(
            "suspense",
            "Suspense",
            ShowingAttributeType.Genre
        );
        public static readonly ShowingAttribute Hindi = new(
            "hindi",
            "Hindi",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Malayalam = new(
            "malayalam",
            "Malayalam",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Tamil = new(
            "tamil",
            "Tamil",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Telugu = new(
            "telugu",
            "Telugu",
            ShowingAttributeType.Language
        );
        public static readonly ShowingAttribute Urdu = new(
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
