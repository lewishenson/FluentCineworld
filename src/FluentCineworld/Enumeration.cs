using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentCineworld
{
    public abstract class Enumeration : IComparable
    {
        private readonly int value;
        private readonly string displayName;

        protected Enumeration()
        {
        }

        protected Enumeration(int value, string displayName)
        {
            this.value = value;
            this.displayName = displayName;
        }

        public int Value => this.value;

        public string DisplayName => this.displayName;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var type = typeof(T);
            var fields = type.GetRuntimeFields();

            return fields.Select(field => field.GetValue(null)).Cast<T>();
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => string.Compare(item.DisplayName, displayName, StringComparison.OrdinalIgnoreCase) == 0);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new InvalidOperationException(message);
            }

            return matchingItem;
        }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = this.GetType().Equals(obj.GetType());
            var valueMatches = this.value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public int CompareTo(object other)
        {
            return this.value.CompareTo(((Enumeration)other).Value);
        }
    }
}