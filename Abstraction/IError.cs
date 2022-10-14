namespace Abd.Shared.Abstraction;

public interface IError
{
    public string? Property { get; set; }
    public string Message { get; }
    public string? Code { get; }
    public string? Description { get; }
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