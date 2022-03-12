using System.Globalization;
using System.Text;

namespace Abd.Shared.Core.Encodings;

public static class Base64Utils
{
    public static string ToBase64Key(long number) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", number)));
}