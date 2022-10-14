namespace Abd.Shared.Abstraction;

public interface IPageInfo
{    
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
    string? StartCursor { get; }
    string? EndCursor { get; }
}