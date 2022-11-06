namespace Abd.Shared.Core.ReactiveExtensions;

public static class ResultExtensions
{
    public static IDisposable OnResult<T>(this IObservable<IResult<T>> observable, Action<T?>? after = null) where T : class, IModel
    {
        if (observable is IConnectableObservable<IResult<T>> connectable)
        {
            observable
                .Where(x => x.IsSuccess)
                .Subscribe(result => after?.Invoke(result.Value));

            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => after?.Invoke(result.Value));
    }

    public static IDisposable OnResult<T>(this IObservable<T> observable, Action? after = null) where T : IResult
    {
        if (observable is IConnectableObservable<IResult> connectable)
        {
            observable
                .Where(x => x.IsSuccess)
                .Subscribe(result => after?.Invoke());

            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(result => after?.Invoke());
    }
    public static IDisposable OnResult<T>(this IObservable<T>  observable, IViewModel viewModel, bool? preLoading = null, bool? loading = null, Action? after = null) where T : IResult
    {
        if (observable is IConnectableObservable<IResult> connectable)
        {
            observable
                .Where(x => x.IsSuccess)
                .Subscribe(result => after?.Invoke());
           observable.SetIfDisableLoader(viewModel:viewModel, preLoading: preLoading, loading: loading);
            return connectable
                .Connect();
        }
        
        return observable
            .Where(x => x.IsSuccess)
            .Subscribe(result =>
            {
                after?.Invoke();
                if(preLoading.HasValue)
            
                    observable
                        .SwitchPreLoading(viewModel, false);
            
                if(loading.HasValue)
            
                    observable
                        .SwitchLoading(viewModel, false);
            });
    }
}