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
    private Action? _onStateChange;
    public IObservable<bool> Disposed0 => _disposed.AsObservable();

    private bool _preLoading = true;
    public bool PreLoading
    {
        get => _preLoading;
        set
        {
            if (_preLoading == value) return;
            
            _preLoading = value;
            StateHasChanged();
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
            StateHasChanged();
        }
    }
    protected readonly ObservableParameters Param0 = new();
    
    protected void StateHasChanged() => _onStateChange?.Invoke();
    

    private readonly ReplaySubject<IEnumerable<IError>> _errorSubject = new(1);
    public IObservable<IEnumerable<IError>> Errors0 => _errorSubject;

    public void OnErrors(IEnumerable<IError>? inputErrors)
    {
        if (inputErrors is null) return;
        
        var errors = inputErrors.ToList();
        if (errors.Any())
        {
            _errorSubject.OnNext(errors);
        }
            
    }

    public IObservable<T> WhenResult0<T>(Func<IObservable<T>> select) where T : IResult
    {
        var observable = select.Invoke();
        return observable
            .TakeUntil(Disposed0)
            .OnErrorGeneric(this)
            .Publish()
            .OnErrorResult(this);
    }
    public IObservable<T> WhenResult0<T>(bool? preLoading = null, bool? loading = null, Func<IObservable<T>>? select = null) where T : IResult
    {
        if(select is null)
            throw new ArgumentNullException(nameof(select));
        
        if (preLoading.HasValue)
            PreLoading = preLoading.Value;
        if (loading.HasValue)
            Loading = loading.Value;
        
        var observable = select.Invoke();
        return observable
            .TakeUntil(Disposed0)
            .OnErrorGeneric(this)
            .Publish()
            .OnErrorResult(this);
    }

    public IObservable<T> WhenAny0<T>(Func<IObservable<T>> select)
    {
        var observable = select.Invoke();
        return observable
            .TakeUntil(Disposed0)
            .OnErrorGeneric(this)
            .Publish();
    }
    public IObservable<T> WhenAny0<T>(bool? preLoading = null, bool? loading = null, Func<IObservable<T>>? select = null)
    {
        if(select is null)
            throw new ArgumentNullException(nameof(select));
        if (preLoading.HasValue)
            PreLoading = true;

        if (loading.HasValue)
            Loading = true;

        var observable = select.Invoke();
        var conObservable = observable
            .TakeUntil(this.Disposed0)
            .OnErrorGeneric(this)
            .Publish();
        
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