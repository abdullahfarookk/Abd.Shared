using Abd.Shared.Core.Errors;

namespace Abd.Shared.Core.Results;

public class Result<T>
{
    public T Value { get; set; }
    public bool IsValid { get; set; }
    public ResultError Error { get; set; }

    public static Result<T> CreateResult(T value) =>
        new Result<T>
        {
            Value = value,
            IsValid = true
        };
    public static Result<T> CreateError(Type type, string message) =>
        new Result<T>
        {
            Error = new ResultError() { ErrorType = type, Message = message },
            IsValid = false
        };
}