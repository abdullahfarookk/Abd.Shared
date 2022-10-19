namespace Abd.Shared.Abstraction.ViewModels;

public interface IViewModel: INotifyPropertyChanged, IDisposable
{

    public Action? OnStateChange { get; set; }
    public IObservable<bool> Disposed0 { get; }
    public bool ComponentLoading { get; set; }
    public bool Loading { get; set; }
    public IObservable<IEnumerable<IError>> Errors0 { get; }
    
    Task OnInitAsync();
    void SetParameters(IReadOnlyDictionary<string, object>? parameters);
    void OnErrors(IEnumerable<IError>? errors);
    
    IObservable<T> Create0<T>(IObservable<T> observable);
    IObservable<T> Create0<T>(Func<IObservable<T>> observable);
}