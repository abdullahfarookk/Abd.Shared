namespace Abd.Shared.Abstraction;

public interface IError
{
    public string Message { get; }
    public string? Code { get; }
    public string? Description { get; }
    public string? StackTrace { get; }
    public Exception? Exception { get;}
    public Severity Severity { get; set; }

}

public enum Severity
{
    /// <summary>Error</summary>
    Error,
    /// <summary>Warning</summary>
    Warning,
    /// <summary>Info</summary>
    Info,
}