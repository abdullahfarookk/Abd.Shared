using System.Text.RegularExpressions;

namespace Abd.Shared.Utils.StringUtils;

public static class ToBase64Utils
{
    public static bool IsBase64(this string base64)
    {
        return Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$");
    }
}