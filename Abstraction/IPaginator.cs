using System.ComponentModel;

namespace Abd.Shared.Abstraction;

public interface IPaginator:INotifyPropertyChanged
{   
    List<byte> PageSizeOptions { get; }
    
    public int? First { get; }
    public int? Last { get; }
    public string? Before { get; }
    public string? After { get; }
    
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
    string? StartCursor { get; }
    string? EndCursor { get; }
    
    public int? TotalCount { get; set; }
    public byte PageSize { get; }
    public byte PageIndex { get; }

    void ChangeSize(byte pageSize);
    void NextPage();
    void PrevPage();
    void FirstPage();
    void LastPage();
    void ChangePageInfo(IPageInfo? pageInfo,int? totalCount = null);
}

