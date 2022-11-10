using System.Collections;
using Abd.Shared.Core.Utils;

namespace Abd.Shared.Core;

public class Result:IResult
{
    public bool IsSuccess { get; }
    public object? Value { get; }
    public IEnumerable<IError> Errors { get; }  = Enumerable.Empty<IError>();
    public string StatusCode => IsSuccess ? "200" : Errors.FirstOrDefault()?.Code??"400";
    public string Message => Errors.FirstOrDefault()?.Message??"An error occured";

    protected Result(object? value)
    {
        IsSuccess = true;
        Value = value;
    }

    protected Result(IEnumerable<IError>? errors)
    {
        IsSuccess = false;
        var errorLst = errors?.ToList();
        if(errorLst is {} && errorLst.Any())
            Errors = errorLst;
    }
    public static IResult<IEnumerable<T>> ParseEnumerable<T>(IOperationResult request) where T : class
    {
        var isSuccess = request.IsSuccessResult();
        if(!isSuccess)
            return FailEnumerable<T>(request.Errors.Adapt<IError>());
        
        var nodes = request.Data?
            .GetType()
            .GetProperties()
            .Select(x 
                => x.GetValue(request.Data))
            .FirstOrDefault() as IEnumerable;
        
        var data = from object value in nodes.AsNotNull()
            select value is IEnumerable enumerable
                ? enumerable.Cast<object>().CreateInstanceFrom<T>()
                : value.CreateInstanceFrom<T>();
        return Create(data);
    }
    public static IResult<T> Parse<T>(IOperationResult request) where T : class
    {
        var isSuccess = request.IsSuccessResult();
        if(!isSuccess)
            return Fail<T>(request.Errors.Adapt<IError>());
        
        var properties = request.Data?
            .GetType()
            .GetProperties()
            .Select(x 
                => x.GetValue(request.Data))
            .ToList();

        return Create(
            properties is { Count: 1 } ? 
            properties.First().CreateInstanceFrom<T>() : 
            properties.CreateInstanceFrom<T>());
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
    public static IResult<IEnumerable<T>> FailEnumerable<T>(IError? error) where T : class
    {
        return new Result<IEnumerable<T>>(error);
    }
    public static IResult<T> Fail<T>(string? message = null) where T : class
    {
        return new Result<T>(message);
    }
    public static IResult Create(object? value = null)
    {
        return new Result(value);
    }
    public static IResult Fail(IError error)
    {
        return new Result(error);
    }
    public static IResult Fail(string error)
    {
        return new Result(error);
    }

    public static IResult Fail()
    {
        return new Result(Array.Empty<IError>());
    }
    public static IResult Fail(IEnumerable<IError> errors)
    {
        return new Result(errors);
    }
    public static IResult Fail(IEnumerable<Error> errors)
    {
        return new Result(errors);
    }
    
}
public class Result<T>:Result,IResult<T> where T : class
{
    public new T? Value => (T?)base.Value;
    public Result(T? value):base(value){ }
    public Result(IEnumerable<IError> errors):base(errors){ }
    
    public Result(IError? error)
        : base(new []{ error ?? new Error("Something went wrong. Error is not specified") }){ }
    
    public Result(string? error = null)
        :base(new []{new Error(error??"Something went wrong. Error is not specified") }){ }
}