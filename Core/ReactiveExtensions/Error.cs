namespace Abd.Shared.Core.ReactiveExtensions;

public static class ErrorExtensions
{
    public static IObservable<T> OnErrorInternal<T>(this IObservable<T> observable, IViewModel viewModel)
        where T : IResult
    {
        observable.Where(x => !x.IsSuccess)
            .Select(x => x.Errors)
            .TakeUntil(viewModel.Disposed0)
            .Subscribe(x =>
            {
                viewModel.OnErrors(x);
                Observable.Timer(TimeSpan.FromSeconds(5)).TakeUntil(viewModel.Disposed0)
                    .Subscribe(_ => viewModel.ClearErrors());
            });
        return observable;
    }
}