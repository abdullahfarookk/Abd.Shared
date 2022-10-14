namespace Abd.Shared.Abstraction;

public interface IError
{
    public string Message { get; }
    public string? Code { get; }
    public string? Description { get; }
    public Exception? Exception { get;}
    
}