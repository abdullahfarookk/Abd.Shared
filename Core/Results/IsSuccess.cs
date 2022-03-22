public interface IIsSuccess
{
    public bool IsValid { get; set; }
    public IEnumerable<IError>? Errors { get; set; }
}
public class IsSuccess : IIsSuccess
{
    public bool IsValid { get; set; }
    public IEnumerable<IError>? Errors { get; set; }
    public static IsSuccess True() =>
        new()
        {
            IsValid = true
        };
    public static IsSuccess False(string message, int code = 500, Exception? exception = default) =>
        new()
        {
            Errors = new List<ResultError>()
            {
                new ResultError() { Message = message, Code = code, Exception = exception }
            },
            IsValid = false
        };

}