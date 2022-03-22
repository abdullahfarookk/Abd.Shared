using Abd.Shared.Core.Exceptions;

public class ResultError:IError
{
    public string Message { get; set; } = "An Error has occured";
    public string? Property { get; set; }
    public int Code { get; set; } = 500;
    public Severity Severity { get; set; } = Severity.Error;
    public Exception? Exception { get; set; } = new AbdException();
}