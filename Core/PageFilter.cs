namespace Abd.Shared.Core;

public class PageFilterBase:IPageFilter
{
    public IFilter Filters {get; }
    public IPaginator Paginator { get; set; } = new Paginator(5);
    private readonly Subject<bool> _disposed = new();
    private IObservable<bool> Disposed0 => _disposed.AsObservable();
    private readonly Subject<Guid> _forceRefresh = new();
    public IObservable<IPageFilter> Change0 { get; }

    protected PageFilterBase(IFilter filters)
    {
        Filters = filters;
        Change0 = Observable
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
    public new CommonFilter Filters => (base.Filters as CommonFilter)!;
    public new IObservable<IPageFilter<CommonFilter>> Change0 => base.Change0.Cast<IPageFilter<CommonFilter>>();
    public PageFilter():base(new CommonFilter()) { }
    public PageFilter(CommonFilter filter):base(filter) { }
}
public class PageFilter<T> : PageFilterBase, IPageFilter<T> where T: class, IFilter, new()
{
    public new T Filters => (base.Filters as T)!;
    public new IObservable<IPageFilter<T>> Change0 => base.Change0.Cast<IPageFilter<T>>();
    public PageFilter():base(new T()) { }
    public PageFilter(T filter):base(filter) { }
}
