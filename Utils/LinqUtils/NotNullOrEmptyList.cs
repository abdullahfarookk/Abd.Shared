namespace Abd.Shared.Utils.LinqUtils;

public static class NotNullOrEmptyList
{
    public static bool HasAny<T>(this IEnumerable<T> source)
    {
        return (source != null && source.Any());
    }
}