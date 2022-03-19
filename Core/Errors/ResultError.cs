using Abd.Shared.Core.Exceptions;
public class ResultError:IError
{
    public string? Message { get; set; } = "An Error has occured";
    public AbdException ErrorType { get; set; } = new AbdException();
    public string PropertyName { get; set; } = default!;
    public string ErrorMessage { get; set; } = default!;
    public string ErrorCode { get; set; } = "500";
    public int Code { get; set; } = 500;
    public Severity Severity { get; set; } = Severity.Error;
}