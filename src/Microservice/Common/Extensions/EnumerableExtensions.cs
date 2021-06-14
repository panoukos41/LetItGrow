using System.Collections.Generic;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);

            return items;
        }
    }
}