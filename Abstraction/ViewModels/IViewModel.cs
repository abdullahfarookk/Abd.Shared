namespace Abd.Shared.Abstraction.ViewModels;

public interface IViewModel: INotifyPropertyChanged, IDisposable
{

    public Action? OnStateChange { get; set; }
    public IObservable<bool> Disposed0 { get; }
    public bool PreLoading { get; set; }
    public bool Loading { get; set; }
    public IObservable<IEnumerable<IError>> Errors0 { get; }
    
    Task OnInitAsync();
    void SetParameters(IReadOnlyDictionary<string, object>? parameters);
    
    void OnErrors(IEnumerable<IError>? errors);
    IObservable<T> WhenResult0<T>(Func<IObservable<T>> select) where T : IResult;

    IObservable<T> WhenResult0<T>(bool? preLoading = null, bool? loading = null, Func<IObservable<T>>? select = null)
        where T : IResult;

    IObservable<T> WhenAny0<T>(Func<IObservable<T>> select);
    IObservable<T> WhenAny0<T>(bool? preLoading = null, bool? loading = null, Func<IObservable<T>>? select = null);
}