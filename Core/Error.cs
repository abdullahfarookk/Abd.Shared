namespace Abd.Shared.Core;

public class Error:IError
{
    public string Code { get; } = "400";
    public string? Property { get; set; } = null!;
    public string Message { get; } = "An error occurred";
    public string? Description { get; set; }
    public Exception? Exception { get; set; }
    public Severity Severity { get; set; }

    public Error(){}
    public Error(string message)
    {
        Message = message;
    }
    public Error(Exception? exception = null)
    {
        Code = "500";
        Message = exception?.Message??"An error occurred";
        Description = exception?.StackTrace;
        Exception = exception;
    }
    public Error(string? code, string? message, string? description = null, Exception? exception = null)
    {
        Code = code??"400";
        Message = message??"An error occurred";
        Description = description;
        Exception = exception;
    }
    public Error(dynamic clientError)
    {
        Code = clientError.Code;
        Message = clientError.Message;
        Exception = clientError.Exception;
    }
}