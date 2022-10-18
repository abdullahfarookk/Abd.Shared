namespace Abd.Shared.Core;
public class ObservableParameters : IObservableParameters
{
    private Dictionary<string, Subject<object>>? _paramsObservables;

    public IObservable<TValue?> Observe<TValue>(string parameterName)
    {
        _paramsObservables ??= new Dictionary<string, Subject<object>>();

        IObservable<object> observable =
            _paramsObservables.GetOrAdd(parameterName.ToUpperInvariant(), () => new Subject<object>());
        return observable.SelectMany(x =>
        {
            if (x is not string val) return Observable.Return(x).Cast<TValue>();
            if (x is "0")
            {
                return Observable.Return(default(TValue));
            }

            var converter = TypeDescriptor.GetConverter(typeof(TValue));
            var res = (TValue)converter.ConvertFrom(val)!;
            return Observable.Return(res);
        });
    }

    /// <summary>
    /// This is supposed to be called from SetParametersAsync();
    /// </summary>
    public void OnNext(IReadOnlyDictionary<string, object> parameters)
    {
        if (_paramsObservables == null) return;
        foreach (var param in parameters)
        {
            if (_paramsObservables.TryGetValue(param.Key.ToUpperInvariant(), out var observable))
            {
                observable.OnNext(param.Value);
            }
        }
    }
}

public static class ObservableParametersExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
        TKey key, Func<TValue> valueCreator)
    {
        if (dictionary.TryGetValue(key, out var value)) return value;
        
        value = valueCreator();
        dictionary.Add(key, value);

        return value;
    }

    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
        TKey key) where TValue : new()
    {
        return dictionary.GetOrAdd(key, () => new TValue());
    }
    public static IObservable<R> WhereParamSelect<T, R>(this IObservable<IReadOnlyDictionary<string, object>> paramsObs,
        string key, Func<T, IObservable<R>> func)
    {
        return paramsObs
            .Where(x =>
                x.ContainsKey(key))
            .Select(x => (T)x["key"])
            .SelectMany(func);
    }

    public static IObservable<T?> Get<T>(IObservable<IReadOnlyDictionary<string, object>> source, string key,
        T? defaultValue = default)
    {
        return source.Select(x => x.TryGetValue(key, out var value) ? (T)value : defaultValue);
    }
}