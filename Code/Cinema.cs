namespace FluentCineworld
{
    public class Cinema : Enumeration
    {
        public static readonly Cinema Aberdeen = new Cinema(1, "Aberdeen");
        public static readonly Cinema Braintree = new Cinema(2, "Braintree");
        public static readonly Cinema Brighton = new Cinema(3, "Brighton");
        public static readonly Cinema Bristol = new Cinema(4, "Bristol");
        public static readonly Cinema BurtonOnTrent = new Cinema(5, "Burton On Trent");
        public static readonly Cinema BuryStEdmunds = new Cinema(6, "Bury St Edmunds");
        public static readonly Cinema Cambridge = new Cinema(7, "Cambridge");
        public static readonly Cinema Cardiff = new Cinema(8, "Cardiff");
        public static readonly Cinema Castleford = new Cinema(9, "Castleford");
        public static readonly Cinema Chelsea = new Cinema(10, "Chelsea");
        public static readonly Cinema Cheltenham = new Cinema(11, "Cheltenham");
        public static readonly Cinema Ashford = new Cinema(12, "Ashford");
        public static readonly Cinema Chesterfield = new Cinema(14, "Chesterfield");
        public static readonly Cinema Chichester = new Cinema(15, "Chichester");
        public static readonly Cinema Crawley = new Cinema(16, "Crawley");
        public static readonly Cinema Didcot = new Cinema(17, "Didcot");
        public static readonly Cinema Didsbury = new Cinema(18, "Didsbury");
        public static readonly Cinema Dundee = new Cinema(19, "Dundee");
        public static readonly Cinema Eastbourne = new Cinema(20, "Eastbourne");
        public static readonly Cinema Edinburgh = new Cinema(21, "Edinburgh");
        public static readonly Cinema Enfield = new Cinema(22, "Enfield");
        public static readonly Cinema AshtonUnderLyne = new Cinema(23, "Ashton-Under-Lyne");
        public static readonly Cinema Falkirk = new Cinema(24, "Falkirk");
        public static readonly Cinema Feltham = new Cinema(25, "Feltham");
        public static readonly Cinema FulhamRoad = new Cinema(26, "Fulham Road");
        public static readonly Cinema GlasgowParkhead = new Cinema(27, "Glasgow Parkhead");
        public static readonly Cinema GlasgowRenfrewStreet = new Cinema(28, "Glasgow Renfrew Street");
        public static readonly Cinema Hammersmith = new Cinema(30, "Hammersmith");
        public static readonly Cinema Harlow = new Cinema(31, "Harlow");
        public static readonly Cinema Haymarket = new Cinema(32, "Haymarket");
        public static readonly Cinema HighWycombe = new Cinema(33, "High Wycombe");
        public static readonly Cinema Bedford = new Cinema(34, "Bedford");
        public static readonly Cinema Hull = new Cinema(35, "Hull");
        public static readonly Cinema Huntingdon = new Cinema(36, "Huntingdon");
        public static readonly Cinema Ilford = new Cinema(37, "Ilford");
        public static readonly Cinema Ipswich = new Cinema(38, "Ipswich");
        public static readonly Cinema IsleOfWight = new Cinema(39, "Isle Of Wight");
        public static readonly Cinema Jersey = new Cinema(40, "Jersey");
        public static readonly Cinema Liverpool = new Cinema(41, "Liverpool");
        public static readonly Cinema Llandudno = new Cinema(42, "Llandudno");
        public static readonly Cinema Luton = new Cinema(43, "Luton");
        public static readonly Cinema Middlesbrough = new Cinema(44, "Middlesbrough");
        public static readonly Cinema Bexleyheath = new Cinema(45, "Bexleyheath");
        public static readonly Cinema MiltonKeynes = new Cinema(1010861, "Milton Keynes");
        public static readonly Cinema NewportWales = new Cinema(47, "Newport Wales");
        public static readonly Cinema Northampton = new Cinema(48, "Northampton");
        public static readonly Cinema Nottingham = new Cinema(49, "Nottingham");
        public static readonly Cinema Rochester = new Cinema(50, "Rochester");
        public static readonly Cinema Rugby = new Cinema(51, "Rugby");
        public static readonly Cinema Runcorn = new Cinema(52, "Runcorn");
        public static readonly Cinema ShaftesburyAvenue = new Cinema(53, "Shaftesbury Avenue");
        public static readonly Cinema Sheffield = new Cinema(54, "Sheffield");
        public static readonly Cinema Shrewsbury = new Cinema(55, "Shrewsbury");
        public static readonly Cinema BirminghamBroadStreet = new Cinema(56, "Birmingham Broad Street");
        public static readonly Cinema Solihull = new Cinema(57, "Solihull");
        public static readonly Cinema Southampton = new Cinema(58, "Southampton");
        public static readonly Cinema StHelens = new Cinema(59, "St Helens");
        public static readonly Cinema StaplesCorner = new Cinema(60, "Staples Corner");
        public static readonly Cinema Stevenage = new Cinema(61, "Stevenage");
        public static readonly Cinema Stockport = new Cinema(62, "Stockport");
        public static readonly Cinema Swindon = new Cinema(63, "Swindon");
        public static readonly Cinema Wakefield = new Cinema(64, "Wakefield");
        public static readonly Cinema Wandsworth = new Cinema(65, "Wandsworth");
        public static readonly Cinema WestIndiaQuay = new Cinema(66, "West India Quay");
        public static readonly Cinema BoldonTyneandWear = new Cinema(67, "Boldon Tyne and Wear");
        public static readonly Cinema Weymouth = new Cinema(68, "Weymouth");
        public static readonly Cinema Wolverhampton = new Cinema(69, "Wolverhampton");
        public static readonly Cinema WoodGreen = new Cinema(70, "Wood Green");
        public static readonly Cinema Yeovil = new Cinema(71, "Yeovil");
        public static readonly Cinema Bolton = new Cinema(72, "Bolton");
        public static readonly Cinema Bradford = new Cinema(73, "Bradford");
        public static readonly Cinema Dublin = new Cinema(75, "Dublin");
        public static readonly Cinema Haverhill = new Cinema(76, "Haverhill");
        public static readonly Cinema Witney = new Cinema(77, "Witney");
        public static readonly Cinema AberdeenUnionSquare = new Cinema(78, "Aberdeen-Union-Square");
        public static readonly Cinema GreenwichO2 = new Cinema(79, "Greenwich O2");
        public static readonly Cinema ScreeningRoomsCheltenham = new Cinema(80, "Screening Rooms Cheltenham");
        public static readonly Cinema Leigh = new Cinema(83, "Leigh");
        public static readonly Cinema Aldershot = new Cinema(87, "Aldershot");
        public static readonly Cinema GlasgowImaxAtGsc = new Cinema(88, "Glasgow - IMAX at GSC");
        public static readonly Cinema Wembley = new Cinema(89, "Wembley");
        public static readonly Cinema GloucesterQuays = new Cinema(90, "Gloucester Quays");
        public static readonly Cinema StNeots = new Cinema(91, "St. Neots");
        public static readonly Cinema Telford = new Cinema(92, "Telford");
        public static readonly Cinema SwindonRegentCircus = new Cinema(93, "Swindon - Regent Circus");
        public static readonly Cinema Broughton = new Cinema(94, "Broughton");
        public static readonly Cinema GlasgowSilverburn = new Cinema(95, "Glasgow - Silverburn");

        protected Cinema()
        {
        }

        protected Cinema(int id, string displayName)
            : base(id, displayName)
        {
        }
    }
}