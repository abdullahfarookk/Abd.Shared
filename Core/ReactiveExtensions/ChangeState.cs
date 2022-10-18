namespace Abd.Shared.Core.ReactiveExtensions;

public static class ChangeStateExtensions
{
    public static IObservable<T> StateHasChanged<T>(this IObservable<T> observable, IViewModel viewModel)
    {
        observable.Subscribe(_ =>viewModel.StateHasChanged());
        return observable;
    }
    private static void StateHasChanged(this IViewModel viewModel)
        => viewModel.OnStateChange?.Invoke();

}