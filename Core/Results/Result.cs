using Abd.Shared.Core.Exceptions;

public class Result<T> : IResult<T>
{
    public T Value { get; set; } = default!;
    public bool IsValid { get; set; }
    public IEnumerable<IError>? Errors { get; set; }

    public static Result<T> CreateResult(T value) =>
        new()
        {
            Value = value,
            IsValid = true
        };
    public static Result<T> CreateError(AbdException type, string message) =>
        new()
        {
            Errors = new List<ResultError>() 
            {
                new ResultError() { ErrorType = type, Message = message }
            } ,
            IsValid = false
        };
}