namespace Abd.Shared.Abstraction;

public interface IFilterablePaginator:IDisposable
{
    ICommonFilter Filters { get; }
    public IPaginator Paginator { get; set; }
    bool Loading { get; set; }
    void UpdatePageInfo(IPageInfo pageInfo,int? totalCount = null);
    void UpdatePageInfo(IPageResult pageResult);
    void Refresh();
    IObservable<IFilterablePaginator> Change0 { get; }
}
public interface IFilterablePaginator<out T>:IFilterablePaginator where T:ICommonFilter
{
    new T Filters { get; }
}