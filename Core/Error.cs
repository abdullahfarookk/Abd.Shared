using System.Text.Json.Serialization;

namespace Abd.Shared.Core;

public class Error:IError
{
    public string Code { get; } = "400";
    public string? Property { get; set; } = null!;
    public string Message { get; } = "An error occurred";
    public string? StackTrace { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public Exception? Exception { get; set; }
    public Severity Severity { get; set; }

    public Error(){}
    public Error(string message)
    {
        Message = message;
    }
    public Error(Exception? exception = null, int? code = null)
    {
        Code = code?.ToString()??"400";
        Message = exception?.Message??"An error occurred";
        StackTrace = exception?.StackTrace;
        Exception = exception;
    }
    public Error(string? code, string? message, string? description, string? stackTrace = null)
    {
        Code = code??"400";
        Message = message??"An error occurred";
        Description = description;
        StackTrace = stackTrace;
    }
    public Error(string? code, string? message, string? description = null, string? stackTrace = null, Exception? exception = null)
    {
        Code = code??"400";
        Message = message??"An error occurred";
        Description = description;
        StackTrace = stackTrace;
        Exception = exception;
    }
    public Error(dynamic clientError)
    {
        Code = clientError.Code;
        Message = clientError.Message;
        Exception = clientError.Exception;
    }
}