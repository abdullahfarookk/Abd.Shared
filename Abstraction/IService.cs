namespace Abd.Shared.Abstraction;

public interface IService
{
    IObservable<IEnumerable<IError>?> Errors0 { get; }

    IObservable<bool> Disposed0 { get; }
    public void ClearErrors();
    void OnErrors(IEnumerable<IError> select);
    
}