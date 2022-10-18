namespace Abd.Shared.Core.ReactiveExtensions;

public static class ResponseExtensions
{
    public static IObservable<IResult> MapResponse(this IObservable<IOperationResult> observable)
        => observable
            .Select(Result.Parse);

    public static IObservable<IResult<T>> MapResponse<T>(this IObservable<IOperationResult> observable)
        where T : class
        => observable.Select(Result.Parse<T>);
 

    public static IObservable<IPageResult<T>> MapPagination<T>(this IObservable<IOperationResult> observable)
        where T : class, IModel =>
        observable.Select(PageResult.Parse<T>);
}