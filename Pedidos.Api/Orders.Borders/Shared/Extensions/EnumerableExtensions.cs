using System.Diagnostics.CodeAnalysis;

namespace Orders.Borders.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HaveDuplicates<TSource, TId>(this IEnumerable<TSource> items, Func<TSource, TId> idFactory)
        {
            if (items == null || items.Count() <= 1)
                return false;

            return !items.Select(idFactory).All(new HashSet<TId>().Add);
        }

        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? list)
        {
            return list is null || !list.Any();
        }
    }
}
