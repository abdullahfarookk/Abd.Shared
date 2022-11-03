namespace Abd.Shared.Core.ReactiveExtensions;

public static class SuccessExtensions
{      
    public static IDisposable OnComplete<T>(this IObservable<T> observable, IViewModel viewModel,
        bool? componentLoading = null,bool? loading = null,
        Action<T>? afterSuccess = null) where T : IResult
    {

        if (observable is IConnectableObservable<T> connectable)
        {
            observable.OnSuccessInternal(afterSuccess);
            
            if(componentLoading.HasValue)
                observable.SwitchComponentLoading(viewModel, componentLoading.Value);
            
            if(loading.HasValue)
                observable.SwitchLoading(viewModel, loading.Value);
            
            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(result =>
            {
                afterSuccess?.Invoke(result);
                
                if (componentLoading.HasValue)
                    viewModel.ComponentLoading = componentLoading.Value;
                
                if(loading.HasValue)
                    viewModel.Loading = loading.Value;
            });
    }
    public static IDisposable OnComplete<T>(this IObservable<T> observable,
        Action<T>? afterSuccess = null) where T : IResult
    {

        if (observable is IConnectableObservable<T> connectable)
        {
            observable.OnSuccessInternal(afterSuccess);
            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => afterSuccess?.Invoke(result));
    }
    
    public static IDisposable OnComplete<T>(this IObservable<IPageResult<T>> observable, IViewModel viewModel, bool? initLoading = null, 
        bool? loading = null, Action<IPageResult<T>>? after = null) where T : IModel
    {
        void Invoke(IPageResult<T> result)
        {
            after?.Invoke(result);
            if (initLoading.HasValue)
                viewModel.ComponentLoading = initLoading.Value;
            if (loading.HasValue)
                viewModel.Loading = loading.Value;
        }

        if (observable is IConnectableObservable<IPageResult<T>> connectable)
        {
            observable
                .Where(x => x.IsSuccess)
                .Subscribe(Invoke);

            
            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(Invoke);
    }
    public static IDisposable OnComplete<T>(this IObservable<IResult<T>> observable, IViewModel viewModel, bool? initLoading = null, 
        bool? loading = null, Action<T?>? after = null) where T : IModel
    {
        
        if (observable is IConnectableObservable<IResult<T>> connectable)
        {
            observable
                .Where(x => x.IsSuccess)
                .Subscribe(result => after?.Invoke(result.Value));
            
            if (initLoading.HasValue)
                observable.SwitchComponentLoading(viewModel, initLoading.Value);
            if(loading.HasValue)
                observable.SwitchLoading(viewModel,loading.Value);
            
            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(result =>
            {
                after?.Invoke(result.Value);
                if(initLoading.HasValue)
                    viewModel.ComponentLoading = initLoading.Value;
                if (loading.HasValue)
                    viewModel.Loading = loading.Value;
            });
    }
    public static IDisposable OnSuccess<T>(this IObservable<T> observable, IViewModel viewModel,
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