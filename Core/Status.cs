namespace Abd.Shared.Core;

public enum Status
{
    InActive,
    Active,
    InProgress,
    Closed
}

public static class EnumUtils
{
    public static Status ToStatus(this int status) => status switch
    {
        0 => Status.InActive,
        1 => Status.Active,
        2 => Status.InProgress,
        3 => Status.Closed,
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };
}