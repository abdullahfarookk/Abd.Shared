namespace Abd.Shared.Core.ReactiveExtensions;

public static class LoadingExtensions
{
    public static IObservable<T> SetIfActiveLoader<T>(this IObservable<T> observable, IViewModel viewModel, bool activeLoader,bool value)
    {
       return activeLoader
            ? observable.SwitchLoading(viewModel, value)
            : observable.StateHasChanged(viewModel);
    }
    public static IObservable<T> SwitchComponentLoading<T>(this IObservable<T> observable, IViewModel viewModel, bool value)
    {
        observable.Subscribe(_ =>viewModel.ComponentLoading = value);
        return observable;
    }
    public static IObservable<T> SwitchLoading<T>(this IObservable<T> observable, IViewModel viewModel, bool value)
    {
        observable.Subscribe(_ =>viewModel.Loading = value);
        return observable;
    }
}