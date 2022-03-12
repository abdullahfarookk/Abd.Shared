namespace Abd.Shared.Utils.StringUtils;

public static class SplitUtils
{
    public static string[] Split(this string value, string symbol)
    => value.Split(new string[] { symbol }, StringSplitOptions.None);
}
