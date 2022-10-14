namespace Abd.Shared.Abstraction;

public interface IPageFilter:IDisposable
{
    ICommonFilter Filters { get; }
    public IPaginator Paginator { get; set; }
    bool Loading { get; set; }
    void UpdatePageInfo(IPageInfo pageInfo,int? totalCount = null);
    void UpdatePageInfo(IPageResult pageResult);
    void Refresh();
    IObservable<IPageFilter> Change0 { get; }
}
public interface IPageFilter<out T>:IPageFilter where T:ICommonFilter
{
    new T Filters { get; }
}