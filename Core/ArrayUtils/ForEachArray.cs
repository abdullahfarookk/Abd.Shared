namespace Abd.Shared.Core.ArrayUtils;

public static class ForEachArray
{
    public static void ForEach<T>(this T[] array,Action<T> action) => Array.ForEach(array, action);

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
        {
            action(item);
        }
    }
}