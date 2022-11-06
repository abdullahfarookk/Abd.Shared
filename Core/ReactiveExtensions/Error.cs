namespace Abd.Shared.Core.ReactiveExtensions;

public static class ErrorExtensions
{
    public static IObservable<T> OnErrorInternal<T>(this IObservable<T> observable, IViewModel viewModel)
        where T : IResult
    {
        observable.Subscribe(_ => { }, ex =>viewModel.OnErrors(new[] { new Error(ex) }));
        
        observable
            .Where(x => !x.IsSuccess)
            .Select(x => x.Errors)
            .Subscribe(viewModel.OnErrors);
        return observable;
    }
    public static IObservable<T> OnErrorGeneric<T>(this IObservable<T> observable, IViewModel viewModel)
    {
        return observable
            .Do(_ => { }, ex => viewModel.OnErrors(new []{new Error(ex)}));
    }
    public static IObservable<T> OnErrorResult<T>(this IObservable<T> observable, IViewModel viewModel) where T : IResult
    {
        observable.Where(x => !x.IsSuccess)
            .Select(x => x.Errors)
            .Subscribe(viewModel.OnErrors);
        
        return observable;
    }
}