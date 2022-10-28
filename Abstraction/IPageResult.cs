namespace Abd.Shared.Abstraction;

public interface IPageResult:IResult
{
    public IPageInfo PageInfo { get; }
    public int? TotalCount { get; }
    
}
public interface IPageResult<out T>:IPageResult,IResult<T>
{ 
    public new IEnumerable<T> Value { get; }
}
