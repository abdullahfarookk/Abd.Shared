namespace Abd.Shared.Core.ReactiveExtensions;

public static class SelectManyExtensions
{
    public static IObservable<TR> SelectFrom<T,TR>(this IObservable<T> observable, IViewModel viewModel,
        Func<T,IObservable<TR>> select,bool? preLoading = null,bool? loading = null) where TR : IResult

    {
        if(preLoading.HasValue)
            viewModel.PreLoading = preLoading.Value;
        if (loading.HasValue)
            viewModel.Loading = loading.Value;
        
        return observable
            .TakeUntil(viewModel.Disposed0)
            .OnErrorGeneric(viewModel)
            .SelectMany(select)
            .Publish()
            .OnErrorResult(viewModel);
    }
}