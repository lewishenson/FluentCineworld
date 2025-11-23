using System;

namespace FluentCineworld
{
    public static class StringExtensions
    {
        public static string Replace(
            this string source,
            string oldValue,
            string newValue,
            StringComparison comparison
        )
        {
            int index = source.IndexOf(oldValue, comparison);

            while (index > -1)
            {
                source = source.Remove(index, oldValue.Length);
                source = source.Insert(index, newValue);

                index = source.IndexOf(oldValue, index + newValue.Length, comparison);
            }

            return source;
        }
    }
}
