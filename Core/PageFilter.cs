namespace Abd.Shared.Core;

public class PageFilterBase:IPageFilter
{
    public IFilter Filters { get; } = new CommonFilter();
    public IPaginator Paginator { get; set; } = new Paginator(5);
    private readonly Subject<bool> _disposed = new();
    private IObservable<bool> Disposed0 => _disposed.AsObservable();
    private readonly Subject<Guid> _forceRefresh = new();

    public IObservable<IPageFilter> Change0 { get; }

    protected PageFilterBase()
    {
        Change0  = Observable
            .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                handler => handler.Invoke,
                h => Paginator.PropertyChanged += h,
                h => Paginator.PropertyChanged -= h)
            .Select(x => this)
            .Merge(_forceRefresh.Select(x => this))
            .Merge(Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    handler => handler.Invoke,
                    h => Filters.PropertyChanged += h,
                    h => Filters.PropertyChanged -= h)
                .Select(x => this))
            .Throttle(TimeSpan.FromMilliseconds(500))
            // .DistinctUntilChanged()
            .TakeUntil(Disposed0);
    }
    public void UpdatePageInfo(IPageInfo pageInfo, int? totalCount = null)
    {
        Paginator.ChangePageInfo(pageInfo,totalCount);
    }

    public void UpdatePageInfo(IPageResult pageResult)
    {
        Paginator.ChangePageInfo(pageResult.PageInfo,pageResult.TotalCount);
    }

    public void Refresh()
    {
        _forceRefresh.OnNext(Guid.NewGuid());
    }
    public void Dispose()
    {
        _disposed.OnNext(true);
    }
}
public class PageFilter :PageFilterBase, IPageFilter<CommonFilter>
{
    public new CommonFilter Filters { get; } = new();
}
public class PageFilter<T> : PageFilterBase, IPageFilter<T> where T:IFilter, new()
{
    public new T Filters { get; } = new();
}
