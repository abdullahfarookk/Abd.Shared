namespace Abd.Shared.Abstraction;

public interface IViewModel:IDisposable
{
    // event PropertyChangedEventHandler PropertyChanged;
    IEnumerable<IError>? ServerErrors { get; set; }
    public Action? OnStateChange { get; set; }
    public IObservable<bool> Disposed0 { get; }
    public bool Loading { get; set; }
    
    Task OnInitAsync();
    void SetParameters(IReadOnlyDictionary<string, object>? parameters);
    void OnErrors(IEnumerable<IError>? errors);
    void ClearErrors();
}