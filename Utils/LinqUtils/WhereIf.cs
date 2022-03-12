namespace Abd.Shared.Utils.LinqUtils;

public static class WhereUtils
{
    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate) 
        => condition ? source.Where(predicate) : source;

    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate) 
        => condition ? source.Where(predicate) : source;
}