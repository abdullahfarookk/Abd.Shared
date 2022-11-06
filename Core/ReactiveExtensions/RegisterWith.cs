namespace Abd.Shared.Core.ReactiveExtensions;

public static class BindWithExtensions
{
    public static IObservable<T> RegisterWith<T>(this IObservable<T> observable, IViewModel viewModel,bool? preLoading = null, bool? loading = null) where T : IResult
    {
        if(preLoading.HasValue)
            viewModel.PreLoading = preLoading.Value;
        if (loading.HasValue)
            viewModel.Loading = loading.Value;
        
        return observable
            .TakeUntil(viewModel.Disposed0)
            .OnErrorGeneric(viewModel)
            .Publish()
            .OnErrorResult(viewModel);

    }
    public static IObservable<T> Register<T>(this IObservable<T> observable, IViewModel viewModel,bool? preLoading = null, bool? loading = null)
    {
        if(preLoading.HasValue)
            viewModel.PreLoading = preLoading.Value;
        if (loading.HasValue)
            viewModel.Loading = loading.Value;
        
        return observable
            .TakeUntil(viewModel.Disposed0)
            .OnErrorGeneric(viewModel)
            .Publish();

    }
}