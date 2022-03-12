using Abd.Shared.Core.Errors;

namespace Abd.Shared.Core.Results;

public class Result<T>
{
    public T Value { get; set; } = default!;
    public bool IsValid { get; set; }
    public ResultError Error { get; set; } = null!;

    public static Result<T> CreateResult(T value) =>
        new()
        {
            Value = value,
            IsValid = true
        };
    public static Result<T> CreateError(Type type, string message) =>
        new()
        {
            Error = new ResultError() { ErrorType = type, Message = message },
            IsValid = false
        };
}