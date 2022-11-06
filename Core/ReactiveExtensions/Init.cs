namespace Abd.Shared.Core.ReactiveExtensions;

public static class InitExtensions
{
    public static IDisposable OnInit<T>(this IObservable<IResult<T>> observable, IViewModel viewModel,
        Action<T?>? after= null) where T : IModel
    {

        viewModel.PreLoading = true;
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(after)
            .SwitchPreLoading(viewModel,false);
        
        return conObservable.Connect();
    }

    public static IDisposable OnInit<T>(this IObservable<IPageResult<T>> observable, IViewModel viewModel,
        Action<IPageResult<T>>? after = null) where T : IModel
    {
        viewModel.PreLoading = true;
        var conObservable = observable
            .TakeUntil(viewModel.Disposed0)
            .Publish();
        
        conObservable
            .OnErrorInternal(viewModel)
            .OnSuccessInternal(after)
            .SwitchPreLoading(viewModel,false);
        
        return conObservable.Connect();
    }
}