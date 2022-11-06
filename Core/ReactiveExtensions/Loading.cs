namespace Abd.Shared.Core.ReactiveExtensions;

public static class LoadingExtensions
{
    public static IObservable<T> SetIfActiveLoader<T>(this IObservable<T> observable, IViewModel viewModel, bool? preLoading = null, 
        bool? loading = null)
    {
        if(preLoading.HasValue)
            observable.SwitchPreLoading(viewModel, true);
            
        if(loading.HasValue)
            observable.SwitchLoading(viewModel, true);
        
        return observable;
    }
    public static IObservable<T> SetIfDisableLoader<T>(this IObservable<T> observable, IViewModel viewModel, bool? preLoading = null, 
        bool? loading = null)
    {
        if(preLoading.HasValue)
            
            observable
                .SwitchPreLoading(viewModel, false);
            
        if(loading.HasValue)
            
            observable
                .SwitchLoading(viewModel, false);
        
        return observable;
    }
    public static IObservable<T> SetIfActiveLoader<T>(this IObservable<T> observable, IViewModel viewModel, bool activeLoader,bool value)
    {
        return activeLoader
            ? observable.SwitchLoading(viewModel, value)
            : observable.StateHasChanged(viewModel);
    }
    public static IObservable<T> SwitchPreLoading<T>(this IObservable<T> observable, IViewModel viewModel, bool value)
    {
        observable.Subscribe(_ =>viewModel.PreLoading = value);
        return observable;
    }
    public static IObservable<T> SwitchLoading<T>(this IObservable<T> observable, IViewModel viewModel, bool value)
    {
        observable.Subscribe(_ =>viewModel.Loading = value);
        return observable;
    }
}