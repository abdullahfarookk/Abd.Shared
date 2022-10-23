using Abd.Shared.Core.ReactiveExtensions;

namespace Abd.Shared.Core;

/// <summary>
/// A base class for view models
/// </summary>
public abstract class ViewModelBase : IViewModel
{
    /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
    public event PropertyChangedEventHandler? PropertyChanged;
    // property changed event handler
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private readonly Subject<bool> _disposed = new();
    private Action? _onStateChange = null;
    public IObservable<bool> Disposed0 => _disposed.AsObservable();

    private bool _componentLoading = true;
    public bool ComponentLoading
    {
        get => _componentLoading;
        set
        {
            if (_componentLoading == value) return;
            
            _componentLoading = value;
            ChangeState();
        }
    }
    
    private bool _loading;
    public bool Loading
    {
        get => _loading;
        set
        {
            if (_loading == value) return;
            
            _loading = value;
            ChangeState();
        }
    }
    protected readonly ObservableParameters Param0 = new();
    
    public void ChangeState() => _onStateChange?.Invoke();
    

    private readonly ReplaySubject<IEnumerable<IError>> _errorSubject = new(1);
    public IObservable<IEnumerable<IError>> Errors0 => _errorSubject;

    public void OnErrors(IEnumerable<IError>? errors)
    {
        _errorSubject.OnNext(errors??Enumerable.Empty<IError>());
    }

    public IObservable<T> WhenAny0<T>(IObservable<T> observable) where T : IResult
    {
        var conObservable = observable
            .TakeUntil(Disposed0)
            .Publish();

        conObservable
            .OnErrorInternal(this);

        return conObservable;
    }
    public IObservable<T> WhenAny0<T>(IObservable<T> observable, bool activateLoader) where T : IResult
    {
        var conObservable = observable
            .TakeUntil(Disposed0)
            .Publish();
        
         if(activateLoader)
             Loading = activateLoader;
         
         conObservable
            .OnErrorInternal(this);

        return conObservable;
    }


    public void ClearErrors()
    {
        _errorSubject.OnNext(Enumerable.Empty<IError>());
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event
    /// </summary>
    /// <param name="parameters"></param>
    public void SetParameters(IReadOnlyDictionary<string, object>? parameters)
    {
        parameters ??= new Dictionary<string, object>();
        Param0.OnNext(parameters);
    }

    Action? IViewModel.OnStateChange { get => _onStateChange; set => _onStateChange = value; }


    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event
    /// </summary>
    public virtual Task OnInitAsync() => Task.CompletedTask;
    
    public void Dispose()
    {
        _disposed.OnNext(true);
        _onStateChange = null;
    }
}