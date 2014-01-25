namespace LewisHenson.CineworldCinemas
{
    public class Cinema : Enumeration
    {
        public static readonly Cinema Bedford = new Cinema(34, "Bedford");
        public static readonly Cinema MiltonKeynes = new Cinema(46, "Milton Keynes");
        public static readonly Cinema Northampton = new Cinema(48, "Northampton");

        protected Cinema()
        {
        }

        protected Cinema(int id, string displayName)
            : base(id, displayName)
        {
        }
    }
}