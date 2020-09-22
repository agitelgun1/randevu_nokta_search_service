using System;
using System.Collections.Generic;

namespace RandevuNokta.Search.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SubStringStartWith(this string source, string c)
        {
            var index = source.IndexOf(c, StringComparison.Ordinal) + 1;
            if (index == 0) index = source.Length > 200 ? 200 : source.Length;
            return source.Substring(0, index);
        }

        public static string StripTagsCharArray(this string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var let in source)
            {
                switch (let)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (inside) continue;
                array[arrayIndex] = let;
                arrayIndex++;
            }

            return new string(array, 0, arrayIndex);
        }

        public static string StripShortTagCharArray(this string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var let in source)
            {
                switch (let)
                {
                    case '[':
                        inside = true;
                        continue;
                    case ']':
                        inside = false;
                        continue;
                }

                if (inside) continue;
                array[arrayIndex] = let;
                arrayIndex++;
            }

            return new string(array, 0, arrayIndex);
        }

        public static string Join(this List<string> items)
        {
            return string.Join(",", items.ToArray());
        }

        public static string Join(this List<string> items, string delimeter)
        {
            return string.Join(delimeter, items.ToArray());
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
