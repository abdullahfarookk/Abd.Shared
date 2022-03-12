namespace Abd.Shared.Utils.TaskUtils;

public static class ForEach
{
    public static async Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
    {
        foreach (var value in list)
        {
            await func(value);
        }
    }
    public static Task ForEachParallelAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
    => Task.WhenAll(list.Select(x => func(x)));
}
