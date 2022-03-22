
public interface IResult<T> : IIsSuccess
{
    public T Value { get; set; }
}
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
    public static Result<T> CreateError(string message, int code = 500, Exception? exception = default) =>
        new()
        {
            Errors = new List<ResultError>() 
            {
                new ResultError() { Message = message, Code = code, Exception = exception }
            } ,
            IsValid = false
        };
    public static Result<T> CreateError(List<ResultError> errors) =>
        new()
        {
            Errors = errors,
            IsValid = false
        };
}