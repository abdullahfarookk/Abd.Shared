namespace Abd.Shared.Core.StringUtils;

public static class NullOrEmpty
{
    public static bool IsNullOrEmpty(this string? value) =>
        string.IsNullOrEmpty(value);
}