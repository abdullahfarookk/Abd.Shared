namespace Abd.Shared.Utils.StringUtils;

public static class NullOrEmpty
{
    public static bool IsNoE(this string? value) =>
        string.IsNullOrEmpty(value);
}