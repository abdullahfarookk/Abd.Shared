namespace Abd.Shared.Abstraction;

public interface IObservableParameters
{
    IObservable<TValue?> Observe<TValue>(string parameterName);
}