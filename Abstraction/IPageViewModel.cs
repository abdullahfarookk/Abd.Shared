namespace Abd.Shared.Abstraction;

public interface IPageViewModel:IViewModel
{
    bool PageLoading { get; set; }
    IFilterablePaginator FilterablePaginator { get;}
}

public interface IPageViewModel<T> : IPageViewModel where T:IModel
{
    public IEnumerable<T> Data { get; set; }
    
}
public interface IPageViewModel<T, out TR>: IPageViewModel<T> where T : IModel where TR: ICommonFilter
{
    new IFilterablePaginator<TR> FilterablePaginator { get;}
}