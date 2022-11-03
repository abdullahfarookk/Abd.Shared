namespace Abd.Shared.Core.ReactiveExtensions;

public static class BindWithExtensions
{
    public static IObservable<T> RegisterWith<T>(this IObservable<T> observable, IViewModel viewModel,bool? initLoading = null, bool? loading = null) where T : IResult
    {
        if(initLoading.HasValue)
            viewModel.ComponentLoading = initLoading.Value;
        if (loading.HasValue)
            viewModel.Loading = loading.Value;
        
        return observable
            .TakeUntil(viewModel.Disposed0)
            .OnErrorGeneric(viewModel)
            .Publish()
            .OnErrorResult(viewModel);

    }
    public static IObservable<T> Register<T>(this IObservable<T> observable, IViewModel viewModel,bool? initLoading = null, bool? loading = null)
    {
        if(initLoading.HasValue)
            viewModel.ComponentLoading = initLoading.Value;
        if (loading.HasValue)
            viewModel.Loading = loading.Value;
        
        return observable
            .TakeUntil(viewModel.Disposed0)
            .OnErrorGeneric(viewModel)
            .Publish();

    }
}