using System.Globalization;

namespace Abd.Shared.Utils.StringUtils;

public static class TitleCase
{
    private static readonly TextInfo TextInfo;
    static TitleCase()
    {
        TextInfo = new CultureInfo("en-US", false).TextInfo;
    }

    public static string ToTitleCase(this string value) =>
        TextInfo.ToTitleCase(value);
}