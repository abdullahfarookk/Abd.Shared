namespace Abd.Shared.Core.ReactiveExtensions;

public static class SuccessExtensions
{
    public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel,bool? preLoading = null, 
        bool? loading = null, Action<T>? after = null) where T : IResult
    {
        var conObservable = observable.AsConnectableObservable(viewModel, preLoading, loading);
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(after)
            .SetIfDisableLoader(viewModel, preLoading, loading);

        return conObservable.Connect();
    }

    private static IConnectableObservable<T> AsConnectableObservable<T>(this IObservable<T> observable, IViewModel viewModel,
        bool? preLoading = null, bool? loading = null)
    {
        if (preLoading.HasValue)
            viewModel.PreLoading = true;

        if (loading.HasValue)
            viewModel.Loading = true;

        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        
        return conObservable;
    }

    public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel, Action<T>? after = null) where T : IResult
    {
        var conObservable = observable.AsConnectableObservable(viewModel);
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(after);

        return conObservable.Connect();
    }
    public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel,Action? after = null) where T : IResult
    {
        var conObservable = observable.AsConnectableObservable(viewModel);
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(after);
           
        
        return conObservable.Connect();
    }
    public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel, bool? preLoading = null, 
        bool? loading = null, Action? after = null) where T : IResult
    {
        var conObservable = observable.AsConnectableObservable(viewModel, preLoading, loading);
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(after)
            .SetIfDisableLoader(viewModel, preLoading, loading);
        
        return conObservable.Connect();
    }

    internal static IObservable<T> OnSuccessInternal<T>(this IObservable<T> observable, IViewModel viewModel, bool? preLoading = null, 
        bool? loading = null, Action<T>? after = null) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(result =>
            {
                after?.Invoke(result);
                if (preLoading.HasValue)
                    viewModel.PreLoading = false;
            
                if(loading.HasValue)
                    viewModel.PreLoading = false;
            });
        return observable;
    }
    
    internal static IObservable<T> OnSuccessInternal<T>(this IObservable<T> observable,
        Action<T>? after = null) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => after?.Invoke(result));
        return observable;
    }
    internal static IObservable<IResult<T>> OnSuccessInternal<T>(this IObservable<IResult<T>> observable,
        Action<T?>? after = null) where T : IModel
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => after?.Invoke(result.Value));
        return observable;
    }
    private static IObservable<T> OnSuccessInternal<T>(this IObservable<T> observable, IViewModel viewModel, bool? preLoading = null, 
        bool? loading = null,
        Action? after = null) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(_ => {
                after?.Invoke();
                if (preLoading.HasValue)
                    viewModel.PreLoading = false;
            
                if(loading.HasValue)
                    viewModel.Loading = false;
            });
        return observable;
    }
    private static IObservable<T> OnSuccessInternal<T>(this IObservable<T> observable,
        Action? after = null) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(_ => after?.Invoke());
        return observable;
    }
}