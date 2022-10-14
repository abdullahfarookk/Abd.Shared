

namespace Abd.Shared.Core;

public class PageInfo:IPageInfo
{
    public bool HasPreviousPage { get; }
    public bool HasNextPage { get; }
    public string? StartCursor { get; }
    public string? EndCursor { get; }
    public PageInfo() { }

    public PageInfo(dynamic pageInfo)
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
    public bool IsSuccess { get; protected set; }
    public IEnumerable<IError> Errors { get; protected set; } = Enumerable.Empty<IError>();
    public IPageInfo PageInfo { get; protected set; } = new PageInfo();
    public int? TotalCount { get; protected set; }

    public static IPageResult<T> Parse<T>(IOperationResult operationResult) where T : class 
        => operationResult.IsSuccessResult()? 
            FromPage<T>(operationResult.Data) : 
            Fail<T>(operationResult.Errors);

    public static IPageResult<T> FromPage<T>(object? request) where T : class
    {
        var result = request?.GetType().GetProperties().FirstOrDefault()?.GetValue(request)!;
        var type = result?.GetType();
        var totalCount = type?.GetProperty("TotalCount")?.GetValue(result);
        var pageInfo = type?.GetProperty("PageInfo")?.GetValue(result);
        var data = type?.GetProperty("Nodes")?.GetValue(result);
        return new PageResult<T>
        {
            IsSuccess = true,
            TotalCount = totalCount is int i ? i : null,
            PageInfo = new PageInfo(pageInfo!),
            Data = data?.Adapt<List<T>>() as IEnumerable<T>?? Enumerable.Empty<T>()
        };
    }
    public static IPageResult<T> Fail<T>(IEnumerable<IClientError> errors) where T : class 
        => new PageResult<T>(errors);
}
public class PageResult<T>:PageResult,IPageResult<T>
{
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

    public PageResult()
    {
        Data = Enumerable.Empty<T>();
        PageInfo = new PageInfo();
    }

    public PageResult(IEnumerable<IClientError> errors)
    {
        IsSuccess = false;
        Errors = errors.Select(x=> new Error(x.Code,x.Message,exception:x.Exception));

    }
    public PageResult(IEnumerable<T> data,PageInfo? pageInfo = null,int? totalCount = null)
    {
        IsSuccess = true;
        Data = data;
        PageInfo = pageInfo ?? new();
        TotalCount = totalCount;
    }
}
