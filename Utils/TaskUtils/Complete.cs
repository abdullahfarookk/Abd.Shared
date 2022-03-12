namespace Abd.Shared.Utils.ThreadUtils;

public static class Complete
{
    public static Task<TR> OnComplete<T, TR>(this Task<T> task, Func<Task<T>, TR> func) =>
        task.ContinueWith(func, TaskContinuationOptions.OnlyOnRanToCompletion);
}