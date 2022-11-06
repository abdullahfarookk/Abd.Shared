using System.Collections;
using Abd.Shared.Core.Utils;

namespace Abd.Shared.Core;

public class PageInfo:IPageInfo
{
    public bool HasPreviousPage { get; }
    public bool HasNextPage { get; }
    public string? StartCursor { get; }
    public string? EndCursor { get; }
    public PageInfo() { }

    public PageInfo(dynamic? pageInfo)
    {
        HasPreviousPage = pageInfo?.HasPreviousPage;
        HasNextPage = pageInfo?.HasNextPage;
        StartCursor = pageInfo?.StartCursor;
        EndCursor = pageInfo?.EndCursor;
    }
    public PageInfo(bool hasPreviousPage, bool hasNextPage, string? startCursor, string? endCursor)
    {
        HasPreviousPage = hasPreviousPage;
        HasNextPage = hasNextPage;
        StartCursor = startCursor;
        EndCursor = endCursor;
    }
}


public class PageResult : IPageResult
{
    public bool IsSuccess { get; }
    public IEnumerable<IError> Errors { get; } = Enumerable.Empty<IError>();
    public object? Value { get; }
    public IPageInfo PageInfo { get; } = new PageInfo();
    public int? TotalCount { get; }
    protected PageResult(object? value, IPageInfo pageInfo, int? totalCount)
    {
        Value = value;
        PageInfo = pageInfo;
        TotalCount = totalCount;
        IsSuccess = true;
    }

    protected PageResult(IEnumerable<IError>? errors)
    {
        IsSuccess = false;
        var errorList = errors?.ToArray();
        if(errorList is {} && errorList.Any())
            Errors = errorList;
       
    }
    public static IPageResult<T> Parse<T>(IOperationResult operationResult) where T : class 
        => operationResult.IsSuccessResult()? 
            FromPage<T>(operationResult.Data) : 
            Fail<T>(operationResult.Errors);

    private static IPageResult<T> FromPage<T>(object? request) where T : class
    {
        try
        {
            var result = request?.GetType().GetProperties().FirstOrDefault()?.GetValue(request);
            var type = result?.GetType();
            var totalCount = type?.GetProperty("TotalCount")?.GetValue(result);
            var pageInfo = type?.GetProperty("PageInfo")?.GetValue(result);
            var nodes = type?.GetProperty("Nodes")?.GetValue(result) as IEnumerable;

            var data = from object value in nodes.AsNotNull()
                select value is IEnumerable enumerable
                    ? enumerable.Cast<object>().CreateInstanceFrom<T>()
                    : value.CreateInstanceFrom<T>();
            return new PageResult<T>(
                data,
                new PageInfo(pageInfo!), 
                totalCount is int i ? i : null);
        }
        catch (Exception e)
        {
            return new PageResult<T>(new Error(e));
        }

    }
    public static IPageResult<T> Fail<T>(IEnumerable<IClientError> errors) where T : class 
        => new PageResult<T>(errors);
}
public class PageResult<T>:PageResult,IPageResult<T>
{
    public new IEnumerable<T> Value => base.Value as IEnumerable<T>?? Enumerable.Empty<T>();
    public PageResult(IEnumerable<IClientError> errors)
        :base(errors.Select(x=> new Error(x.Code,x.Message,exception:x.Exception))){}
    public PageResult(IEnumerable<IError> errors)
        :base(errors){}
    public PageResult(IError error) : base(new[] {error}) { }
    public PageResult(IEnumerable<T> value,PageInfo? pageInfo = null,int? totalCount = null)
        :base(value,pageInfo?? new PageInfo(),totalCount){ }

    T? IResult<T>.Value => (T?)Value;
}
