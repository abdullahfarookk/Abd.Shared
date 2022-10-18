namespace Abd.Shared.Utils.ListUtils;

public static class AsNotNullUtils
{
    public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T>? list)
    {
        return list ?? Enumerable.Empty<T>();
    }
}