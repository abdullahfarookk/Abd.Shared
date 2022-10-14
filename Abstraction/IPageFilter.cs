namespace Abd.Shared.Abstraction;

public interface IPageFilter:IDisposable
{
    IFilter Filters { get; }
    public IPaginator Paginator { get; set; }
    void UpdatePageInfo(IPageInfo pageInfo,int? totalCount = null);
    void UpdatePageInfo(IPageResult pageResult);
    void Refresh();
    IObservable<IPageFilter> Change0 { get; }
}
public interface IPageFilter<out T>:IPageFilter where T:IFilter
{
    new T Filters { get; }
}