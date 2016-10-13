using System;

namespace FluentCineworld
{
    public class Cinema : Enumeration
    {
        public static readonly Cinema AberdeenQueensLinks = new Cinema(1010804, "Aberdeen - Queens Links");
        public static readonly Cinema AberdeenUnionSquare = new Cinema(1010808, "Aberdeen - Union Square");
        public static readonly Cinema Aldershot = new Cinema(1010805, "Aldershot");
        public static readonly Cinema Ashford = new Cinema(1010806, "Ashford");
        public static readonly Cinema AshtonUnderLyne = new Cinema(1010807, "Ashton-under-Lyne");
        public static readonly Cinema Basildon = new Cinema(1010868, "Basildon");
        public static readonly Cinema Bedford = new Cinema(1010809, "Bedford");
        public static readonly Cinema BirminghamBroadStreet = new Cinema(1010812, "Birmingham - Broad Street");
        public static readonly Cinema BirminghamNEC = new Cinema(1010892, "Birmingham - NEC");
        public static readonly Cinema BoldonTyneAndWear = new Cinema(1010819, "Boldon Tyne and Wear");
        public static readonly Cinema Bolton = new Cinema(1010813, "Bolton");
        public static readonly Cinema Bradford = new Cinema(1010811, "Bradford");
        public static readonly Cinema Braintree = new Cinema(1010815, "Braintree");
        public static readonly Cinema Brighton = new Cinema(1010816, "Brighton");
        public static readonly Cinema Bristol = new Cinema(1010818, "Bristol");
        public static readonly Cinema Bromley = new Cinema(1010803, "Bromley");
        public static readonly Cinema Broughton = new Cinema(1010889, "Broughton");
        public static readonly Cinema BurtonUponTrent = new Cinema(1010814, "Burton upon Trent");
        public static readonly Cinema BuryStEdmunds = new Cinema(1010817, "Bury St Edmunds");
        public static readonly Cinema Cardiff = new Cinema(1010821, "Cardiff");
        public static readonly Cinema Castleford = new Cinema(1010822, "Castleford");
        public static readonly Cinema Cheltenham = new Cinema(1010824, "Cheltenham");
        public static readonly Cinema CheltenhamTheScreeningRooms = new Cinema(1010828, "Cheltenham - The Screening Rooms");
        public static readonly Cinema Chesterfield = new Cinema(1010829, "Chesterfield");
        public static readonly Cinema Chichester = new Cinema(1010823, "Chichester");
        public static readonly Cinema Crawley = new Cinema(1010827, "Crawley");
        public static readonly Cinema DaltonPark = new Cinema(1010899, "Dalton Park");
        public static readonly Cinema Didcot = new Cinema(1010830, "Didcot");
        public static readonly Cinema Didsbury = new Cinema(1010831, "Didsbury");
        public static readonly Cinema Dundee = new Cinema(1010832, "Dundee");
        public static readonly Cinema Eastbourne = new Cinema(1010833, "Eastbourne");
        public static readonly Cinema Edinburgh = new Cinema(1010834, "Edinburgh");
        public static readonly Cinema Falkirk = new Cinema(1010836, "Falkirk");
        public static readonly Cinema GlasgowScienceCentre = new Cinema(1010844, "Glasgow - IMAX at GSC");
        public static readonly Cinema GlasgowParkhead = new Cinema(1010839, "Glasgow - Parkhead");
        public static readonly Cinema GlasgowRenfrewStreet = new Cinema(1010843, "Glasgow - Renfrew Street");
        public static readonly Cinema GlasgowSilverburn = new Cinema(1010891, "Glasgow - Silverburn");
        public static readonly Cinema GloucesterQuays = new Cinema(1010841, "Gloucester Quays");
        public static readonly Cinema Harlow = new Cinema(1010846, "Harlow");
        public static readonly Cinema Haverhill = new Cinema(1010847, "Haverhill");
        public static readonly Cinema HemelHempstead = new Cinema(1010826, "Hemel Hempstead");
        public static readonly Cinema HighWycombe = new Cinema(1010851, "High Wycombe");
        public static readonly Cinema Hinckley = new Cinema(1010895, "Hinckley");
        public static readonly Cinema Hull = new Cinema(1010849, "Hull");
        public static readonly Cinema Huntingdon = new Cinema(1010850, "Huntingdon");
        public static readonly Cinema Ipswich = new Cinema(1010854, "Ipswich");
        public static readonly Cinema IsleOfWight = new Cinema(1010853, "Isle Of Wight");
        public static readonly Cinema Jersey = new Cinema(1010855, "Jersey");
        public static readonly Cinema Leigh = new Cinema(1010856, "Leigh");
        public static readonly Cinema Llandudno = new Cinema(1010858, "Llandudno");
        public static readonly Cinema LondonBexleyheath = new Cinema(1010810, "London - Bexleyheath");
        public static readonly Cinema LondonChelsea = new Cinema(1010825, "London - Chelsea");
        public static readonly Cinema LondonEnfield = new Cinema(1010835, "London - Enfield");
        public static readonly Cinema LondonFeltham = new Cinema(1010837, "London - Feltham");
        public static readonly Cinema LondonFulhamRoad = new Cinema(1010838, "London - Fulham Road");
        public static readonly Cinema LondonHaymarket = new Cinema(1010848, "London - Haymarket");
        public static readonly Cinema LondonIlford = new Cinema(1010852, "London - Ilford");
        public static readonly Cinema LondonLeicesterSquare = new Cinema(1010820, "London - Leicester Square");
        public static readonly Cinema LondonTheO2Greenwich = new Cinema(1010842, "London - The O2 Greenwich");
        public static readonly Cinema LondonWandsworth = new Cinema(1010879, "London - Wandsworth");
        public static readonly Cinema LondonWembley = new Cinema(1010880, "London - Wembley");
        public static readonly Cinema LondonWestIndiaQuay = new Cinema(1010882, "London - West India Quay");
        public static readonly Cinema LondonWoodGreen = new Cinema(1010884, "London - Wood Green");
        public static readonly Cinema Loughborough = new Cinema(1010898, "Loughborough");
        public static readonly Cinema Luton = new Cinema(1010859, "Luton");
        public static readonly Cinema Middlesbrough = new Cinema(1010860, "Middlesbrough");
        public static readonly Cinema MiltonKeynes = new Cinema(1010861, "Milton Keynes");
        public static readonly Cinema NewportFriarsWalk = new Cinema(1010893, "Newport - Friars Walk");
        public static readonly Cinema NewportSpyttyPark = new Cinema(1010862, "Newport - Spytty Park");
        public static readonly Cinema Northampton = new Cinema(1010863, "Northampton");
        public static readonly Cinema Nottingham = new Cinema(1010864, "Nottingham");
        public static readonly Cinema Poole = new Cinema(1010840, "Poole");
        public static readonly Cinema Rochester = new Cinema(1010865, "Rochester");
        public static readonly Cinema Rugby = new Cinema(1010866, "Rugby");
        public static readonly Cinema Runcorn = new Cinema(1010867, "Runcorn");
        public static readonly Cinema Sheffield = new Cinema(1010869, "Sheffield");
        public static readonly Cinema Shrewsbury = new Cinema(1010870, "Shrewsbury");
        public static readonly Cinema Solihull = new Cinema(1010871, "Solihull");
        public static readonly Cinema Southampton = new Cinema(1010872, "Southampton");
        public static readonly Cinema StHelens = new Cinema(1010875, "St Helens");
        public static readonly Cinema StNeots = new Cinema(1010887, "St Neots");
        public static readonly Cinema Stevenage = new Cinema(1010874, "Stevenage");
        public static readonly Cinema Stockport = new Cinema(1010876, "Stockport");
        public static readonly Cinema StokeOnTrent = new Cinema(1010896, "Stoke-on-Trent");
        public static readonly Cinema SwindonRegentCircus = new Cinema(1010888, "Swindon - Regent Circus");
        public static readonly Cinema SwindonShawRidge = new Cinema(1010877, "Swindon - Shaw Ridge");
        public static readonly Cinema Telford = new Cinema(1010802, "Telford");
        public static readonly Cinema Wakefield = new Cinema(1010878, "Wakefield");
        public static readonly Cinema Weymouth = new Cinema(1010881, "Weymouth");
        public static readonly Cinema Whiteley = new Cinema(1010894, "Whiteley");
        public static readonly Cinema Witney = new Cinema(1010883, "Witney");
        public static readonly Cinema Wolverhampton = new Cinema(1010885, "Wolverhampton");
        public static readonly Cinema Yate = new Cinema(1010897, "Yate");
        public static readonly Cinema Yeovil = new Cinema(1010886, "Yeovil");

        protected Cinema()
        {
        }

        protected Cinema(int id, string displayName)
            : base(id, displayName)
        {
        }
    }
}