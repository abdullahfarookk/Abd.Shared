namespace Abd.Shared.Core.ReactiveExtensions;

public static class SelectManyExtensions
{
    public static IObservable<TR> SelectFrom<T,TR>(this IObservable<T> observable, IViewModel viewModel,
        Func<T,IObservable<TR>> action,bool? initLoading = null,bool? loading = null) where TR : IResult

    {
        if(initLoading.HasValue)
            viewModel.ComponentLoading = initLoading.Value;
        if (loading.HasValue)
            viewModel.Loading = loading.Value;
        
        return observable
            .TakeUntil(viewModel.Disposed0)
            .OnErrorGeneric(viewModel)
            .SelectMany(action)
            .Publish()
            .OnErrorResult(viewModel);
    }
}