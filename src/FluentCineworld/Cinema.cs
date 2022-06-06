using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentCineworld
{
    public record Cinema
    {
        public static readonly Cinema AberdeenQueensLinks = new("022", "Aberdeen - Queens Links");
        public static readonly Cinema AberdeenUnionSquare = new("074", "Aberdeen - Union Square");
        public static readonly Cinema Aldershot = new("080", "Aldershot");
        public static readonly Cinema Ashford = new("035", "Ashford");
        public static readonly Cinema AshtonUnderLyne = new("068", "Ashton-under-Lyne");
        public static readonly Cinema Basildon = new("100", "Basildon");
        public static readonly Cinema Bedford = new("010", "Bedford");
        public static readonly Cinema Belfast = new("117", "Belfast");
        public static readonly Cinema BirminghamBroadStreet = new("044", "Birmingham - Broad Street");
        public static readonly Cinema BirminghamNec = new("090", "Birmingham - NEC");
        public static readonly Cinema BoldonTyneAndWear = new("024", "Boldon Tyne and Wear");
        public static readonly Cinema Bolton = new("032", "Bolton");
        public static readonly Cinema Bracknell = new("107", "Bracknell");
        public static readonly Cinema Bradford = new("058", "Bradford");
        public static readonly Cinema Braintree = new("061", "Braintree");
        public static readonly Cinema Brighton = new("014", "Brighton");
        public static readonly Cinema Bristol = new("072", "Bristol");
        public static readonly Cinema Broughton = new("089", "Broughton");
        public static readonly Cinema BurtonUponTrent = new("047", "Burton upon Trent");
        public static readonly Cinema BuryStEdmunds = new("070", "Bury St Edmunds");
        public static readonly Cinema Cardiff = new("055", "Cardiff");
        public static readonly Cinema Castleford = new("064", "Castleford");
        public static readonly Cinema Cheltenham = new("069", "Cheltenham");
        public static readonly Cinema Chesterfield = new("029", "Chesterfield");
        public static readonly Cinema Chichester = new("063", "Chichester");
        public static readonly Cinema Crawley = new("034", "Crawley");
        public static readonly Cinema DaltonPark = new("096", "Dalton Park");
        public static readonly Cinema Didcot = new("071", "Didcot");
        public static readonly Cinema Didsbury = new("051", "Didsbury");
        public static readonly Cinema Dover = new("099", "Dover");
        public static readonly Cinema Dundee = new("036", "Dundee");
        public static readonly Cinema EastbourneAtTheBeacon = new("113", "Eastbourne at The Beacon");
        public static readonly Cinema Edinburgh = new("037", "Edinburgh");
        public static readonly Cinema Ely = new("097", "Ely");
        public static readonly Cinema Falkirk = new("052", "Falkirk");
        public static readonly Cinema GlasgowParkhead = new("005", "Glasgow - Parkhead");
        public static readonly Cinema GlasgowRenfrewStreet = new("057", "Glasgow - Renfrew Street");
        public static readonly Cinema GlasgowSilverburn = new("088", "Glasgow - Silverburn");
        public static readonly Cinema GloucesterQuays = new("083", "Gloucester Quays");
        public static readonly Cinema HarlowHarveyCentre = new("098", "Harlow - Harvey Centre");
        public static readonly Cinema HarlowQueensgate = new("013", "Harlow - Queensgate");
        public static readonly Cinema Haverhill = new("076", "Haverhill");
        public static readonly Cinema HemelHempstead = new("102", "Hemel Hempstead");
        public static readonly Cinema HighWycombe = new("073", "High Wycombe");
        public static readonly Cinema Hinckley = new("092", "Hinckley");
        public static readonly Cinema Hull = new("040", "Hull");
        public static readonly Cinema Huntingdon = new("043", "Huntingdon");
        public static readonly Cinema Ipswich = new("028", "Ipswich");
        public static readonly Cinema Jersey = new("062", "Jersey");
        public static readonly Cinema LeedsWhiteRose = new("108", "Leeds - White Rose");
        public static readonly Cinema Leigh = new("078", "Leigh");
        public static readonly Cinema Llandudno = new("053", "Llandudno");
        public static readonly Cinema LondonBexleyheath = new("027", "London - Bexleyheath");
        public static readonly Cinema LondonEnfield = new("048", "London - Enfield");
        public static readonly Cinema LondonFeltham = new("023", "London - Feltham");
        public static readonly Cinema LondonHounslow = new("118", "London - Hounslow");
        public static readonly Cinema LondonIlford = new("060", "London - Ilford");
        public static readonly Cinema LondonLeicesterSquare = new("103", "London - Leicester Square");
        public static readonly Cinema LondonSouthRuislip = new("106", "London - South Ruislip");
        public static readonly Cinema LondonTheO2Greenwich = new("077", "London - The O2 Greenwich");
        public static readonly Cinema LondonWandsworth = new("066", "London - Wandsworth");
        public static readonly Cinema LondonWembley = new("082", "London - Wembley");
        public static readonly Cinema LondonWestIndiaQuay = new("041", "London - West India Quay");
        public static readonly Cinema LondonWoodGreen = new("046", "London - Wood Green");
        public static readonly Cinema Loughborough = new("095", "Loughborough");
        public static readonly Cinema Luton = new("030", "Luton");
        public static readonly Cinema Middlesbrough = new("054", "Middlesbrough");
        public static readonly Cinema MiltonKeynes = new("042", "Milton Keynes");
        public static readonly Cinema NewcastleUponTyne = new("105", "Newcastle upon Tyne");
        public static readonly Cinema NewportIsleofWight = new("045", "Newport - Isle of Wight");
        public static readonly Cinema NewportSpyttyPark = new("026", "Newport - Spytty Park");
        public static readonly Cinema Northampton = new("018", "Northampton");
        public static readonly Cinema Nottingham = new("065", "Nottingham");
        public static readonly Cinema Plymouth = new("114", "Plymouth");
        public static readonly Cinema Poole = new("104", "Poole");
        public static readonly Cinema Rochester = new("020", "Rochester");
        public static readonly Cinema Rugby = new("049", "Rugby");
        public static readonly Cinema Runcorn = new("038", "Runcorn");
        public static readonly Cinema RushdenLakes = new("112", "Rushden Lakes");
        public static readonly Cinema Sheffield = new("031", "Sheffield");
        public static readonly Cinema Shrewsbury = new("033", "Shrewsbury");
        public static readonly Cinema Solihull = new("056", "Solihull");
        public static readonly Cinema Speke = new("110", "Speke");
        public static readonly Cinema Stevenage = new("019", "Stevenage");
        public static readonly Cinema StHelens = new("050", "St Helens");
        public static readonly Cinema StNeots = new("084", "St Neots");
        public static readonly Cinema StokeOnTrent = new("093", "Stoke-on-Trent");
        public static readonly Cinema SwindonRegentCircus = new("086", "Swindon - Regent Circus");
        public static readonly Cinema SwindonShawRidge = new("012", "Swindon - Shaw Ridge");
        public static readonly Cinema Telford = new("085", "Telford");
        public static readonly Cinema Wakefield = new("021", "Wakefield");
        public static readonly Cinema Warrington = new("115", "Warrington");
        public static readonly Cinema Watford = new("111", "Watford");
        public static readonly Cinema WestonSuperMare = new("109", "Weston-super-Mare");
        public static readonly Cinema Weymouth = new("039", "Weymouth");
        public static readonly Cinema Whiteley = new("091", "Whiteley");
        public static readonly Cinema Witney = new("075", "Witney");
        public static readonly Cinema Wolverhampton = new("025", "Wolverhampton");
        public static readonly Cinema Yate = new("094", "Yate");
        public static readonly Cinema Yeovil = new("059", "Yeovil");
        public static readonly Cinema York = new("116", "York");

        private static readonly IDictionary<string, Cinema> AllKeyedById;

        public static IEnumerable<Cinema> All => AllKeyedById.Values;

        static Cinema()
        {
            AllKeyedById = typeof(Cinema).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(Cinema))
                .Select(field => (Cinema)field.GetValue(null))
                .ToDictionary(cinema => cinema!.Id, cinema => cinema);
        }

        private Cinema(string id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public string Id { get; }

        public string DisplayName { get; }

        public static Cinema GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            }

            return AllKeyedById.TryGetValue(id, out var cinema)
                ? cinema
                : throw new ArgumentOutOfRangeException(nameof(id), id, "Cinema not found with specified id");
        }
    }
}
