using System.Text;

namespace LewisHenson.CineworldCinemas.Listings
{
    public class Showing
    {
        public string Time { get; set; }

        public bool Is2D { get; set; }

        public bool Is3D { get; set; }

        public bool IsDBox { get; set; }

        public bool SoldOut
        {
            get
            {
                return !Is2D && !Is3D && !IsDBox;
            }
        }

        public string DisplayText
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            var outputBuilder = new StringBuilder();

            if (Is2D)
            {
                outputBuilder.Append("2D");
            }

            if (Is3D)
            {
                if (outputBuilder.Length > 0)
                {
                    outputBuilder.Append(", ");
                }

                outputBuilder.Append("3D");
            }

            if (IsDBox)
            {
                if (outputBuilder.Length > 0)
                {
                    outputBuilder.Append(", ");
                }

                outputBuilder.Append("DBOX");
            }

            if (SoldOut)
            {
                if (outputBuilder.Length > 0)
                {
                    outputBuilder.Append(", ");
                }

                outputBuilder.Append("Sold Out");
            }

            if (outputBuilder.Length == 0)
            {
                return Time;
            }

            outputBuilder.Insert(0, " (");
            outputBuilder.Insert(0, Time);
            outputBuilder.Append(")");

            return outputBuilder.ToString();
        }
    }
}