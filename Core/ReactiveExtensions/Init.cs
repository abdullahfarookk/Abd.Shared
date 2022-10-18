namespace Abd.Shared.Core.ReactiveExtensions;

public static class InitExtensions
{
    public static IDisposable OnInit<T>(this IObservable<IResult<T>> observable, IViewModel viewModel,
        Action<T?>? afterSuccess = null) where T : IModel
    {
        viewModel.ComponentLoading = true;
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(afterSuccess)
            .SwitchComponentLoading(viewModel,false);
        
        return conObservable.Connect();
    }

    public static IDisposable OnInit<T>(this IObservable<IPageResult<T>> observable, IViewModel viewModel,
        Action<IPageResult<T>>? afterSuccess = null) where T : IModel
    {
        viewModel.ComponentLoading = true;
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(afterSuccess)
            .SwitchComponentLoading(viewModel,false);
        
        return conObservable.Connect();
    }
}