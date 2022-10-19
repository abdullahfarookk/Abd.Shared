namespace Abd.Shared.Core.ReactiveExtensions;

public static class ErrorExtensions
{
    public static IObservable<T> OnErrorInternal<T>(this IObservable<T> observable, IViewModel viewModel)
        where T : IResult
    {
        observable.Where(x => !x.IsSuccess)
            .Select(x => x.Errors)
            .TakeUntil(viewModel.Disposed0)
            .Subscribe(viewModel.OnErrors);
        return observable;
    }
}