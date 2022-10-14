using System.ComponentModel;

namespace Abd.Shared.Abstraction.ViewModels;

public interface IViewModel: INotifyPropertyChanged, IDisposable
{
    IEnumerable<IError>? ServerErrors { get; set; }
    public Action? OnStateChange { get; set; }
    public IObservable<bool> Disposed0 { get; }
    public bool Loading { get; set; }
    
    Task OnInitAsync();
    void SetParameters(IReadOnlyDictionary<string, object>? parameters);
    void OnErrors(IEnumerable<IError>? errors);
    void ClearErrors();
}