namespace Abd.Shared.Core.ReactiveExtensions;

public static class SuccessExtensions
{        public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel,
        Action<T>? afterSuccess = null, bool activateLoader= true) where T : IResult
    {
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        if (activateLoader)
            viewModel.Loading = true;


        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(afterSuccess)
            .SetIfActiveLoader(viewModel, activateLoader, false);

        return conObservable.Connect();
    }
    public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel, 
        Action? afterSuccess = null,bool activateLoader= true) where T : IResult
    {
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();

        if (activateLoader)
            viewModel.Loading = true;


        conObservable
            .Delay(x => Observable
                .Timer(TimeSpan
                    .FromMilliseconds(0.0001)))
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(afterSuccess)
            .SetIfActiveLoader(viewModel, activateLoader, false);
           
        
        return conObservable.Connect();
    }
    public static IDisposable Complete<T>(this IObservable<T> observable, IViewModel viewModel,bool activateLoader= true) where T : IResult
    {
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        if(activateLoader)
            viewModel.Loading = true;
        
        
        conObservable
            .OnErrorInternal(viewModel)
            .CompleteInternal()
            .SwitchLoading(viewModel,false);
        
        return conObservable.Connect();
    }
    public static IObservable<T> OnSuccessInternal<T>(this IObservable<T> observable,
        Action<T>? afterSuccess = null) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => afterSuccess?.Invoke(result));
        return observable;
    }
    public static IObservable<IResult<T>> OnSuccessInternal<T>(this IObservable<IResult<T>> observable,
        Action<T?>? afterSuccess = null) where T : IModel
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => afterSuccess?.Invoke(result.Value));
        return observable;
    }

    private static IObservable<T> OnSuccessInternal<T>(this IObservable<T> observable,
        Action? afterSuccess = null) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe(_ => afterSuccess?.Invoke());
        return observable;
    }
    private static IObservable<T> CompleteInternal<T>(this IObservable<T> observable) where T : IResult
    {
        observable
            .Where(x => x.IsSuccess)
            .Subscribe();
        return observable;
    }
    public static IDisposable Complete<T>(this IObservable<T> observable,Action<T>? action = null)
    {
        var obs = observable.Subscribe(x => action?.Invoke(x));
        if (observable is IConnectableObservable<T> connectableObservable)
            connectableObservable.Connect();
        return obs;
    }
}