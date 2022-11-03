using System.Collections;

namespace Abd.Shared.Core.Utils;

public static class EnumerableExtensions
{
    public static IEnumerable AsNotNull(this IEnumerable? list)
    {
        return list ?? Array.Empty<object>();
    }
    public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T>? list)
    {
        return list ?? Array.Empty<T>();
    }
}