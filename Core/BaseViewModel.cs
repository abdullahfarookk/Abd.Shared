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
    
    public void ChangeState() => OnStateChange?.Invoke();
    

    private readonly Subject<IEnumerable<IError>> _errorSubject = new();
    public IObservable<IEnumerable<IError>> Errors0 => _errorSubject;

    public void OnErrors(IEnumerable<IError>? errors)
    {
        _errorSubject.OnNext(errors??Enumerable.Empty<IError>());
    }

    public void ClearErrors()
    {
        _errorSubject.OnNext(Enumerable.Empty<IError>());
    }

    public IEnumerable<IError>? ServerErrors { get; set; }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event
    /// </summary>
    /// <param name="parameters"></param>
    public void SetParameters(IReadOnlyDictionary<string, object>? parameters)
    {
        parameters ??= new Dictionary<string, object>();
        Param0.OnNext(parameters);
    }

    public Action? OnStateChange { get; set; }


    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event
    /// </summary>
    public virtual Task OnInitAsync() => Task.CompletedTask;
    protected void MapErrorResult(IEnumerable<IError> errors)
    {
        OnErrors(errors);
        Observable.Timer(TimeSpan.FromSeconds(5)).TakeUntil(Disposed0)
            .Subscribe(_ => ClearErrors());
    }
    

    public void Dispose()
    {
        _disposed.OnNext(true);
        OnStateChange = null;
    }
}