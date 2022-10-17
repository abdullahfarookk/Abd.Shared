namespace Abd.Shared.Core;

public class Result:IResult
{
    public bool IsSuccess { get; protected set; }
    public object? Value { get; protected set; }
    public IEnumerable<IError> Errors { get; protected set; }  = Enumerable.Empty<IError>();
    public string StatusCode => IsSuccess ? "200" : Errors.FirstOrDefault()?.Code??"400";
    public string Message => Errors.FirstOrDefault()?.Message??"An error occured";
    
    public static IResult<T> Parse<T>(IOperationResult request) where T : class
    {
        return request
            .IsSuccessResult() ? 
            
            Create(request.Data?
                .GetType()
                .GetProperties()
                .FirstOrDefault()?
                .GetValue(request.Data)?
                .Adapt<T>()) : 
            
            Fail<T>(request?
                .Errors?
                .Adapt<IError>());
    }
    public static IResult Parse(IOperationResult result) 
        => result.IsSuccessResult() ? 
            Create() : 
            Fail(result.Errors.Adapt<IEnumerable<Error>>());

    public static IResult<T> Create<T>(T? data) where T : class 
        => new Result<T>(data);

    public static IObservable<IResult<T>> Create0<T>(T? data) where T : class 
        => Observable.Return<IResult<T>>(new Result<T>(data));

    // errors
    public static IResult<T> Fail<T>(IEnumerable<IError> errors) where T : class
    {
        return new Result<T>(errors);
    }
    // single error
    public static IResult<T> Fail<T>(IError? error) where T : class
    {
        return new Result<T>(error);
    }
    public static IResult<T> Fail<T>(string? message = null) where T : class
    {
        return new Result<T>(message);
    }
    public static IResult Create(object? value = null)
    {
        return new Result {IsSuccess = true, Value = value};
    }
    public static IResult Fail(IError error)
    {
        return new Result {IsSuccess = false, Errors = new List<IError> {error}};
    }
    public static IResult Fail(string error)
    {
        return new Result {IsSuccess = false, Errors = new List<IError> {new Error(error)}};
    }

    public static IResult Fail()
    {
        return new Result {IsSuccess = false, Errors = new List<IError> {new Error()}};
    }
    public static IResult Fail(IEnumerable<IError> errors)
    {
        return new Result {IsSuccess = false, Errors = errors};
    }
    public static IResult Fail(IEnumerable<Error> errors)
    {
        return new Result {IsSuccess = false, Errors = errors};
    }
    
}
public class Result<T>:Result,IResult<T> where T : class
{
    public Result(T? value)
    {
        Value = value;
        IsSuccess = true;
    }
    public Result(IEnumerable<IError> errors)
    {
        Errors = errors;
        IsSuccess = false;
    }
    public Result(IError? error)
    {
        Errors = new List<IError> {error??new Error("Something went wrong. Error is not specified")};
        IsSuccess = false;
    }
    public Result(string? error = null)
    {
        Errors = new List<IError> {new Error(error??"An Error Occurred")};
        IsSuccess = false;
    }
    public new T? Value { get; }
}