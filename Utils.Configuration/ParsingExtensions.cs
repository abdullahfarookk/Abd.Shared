using Microsoft.Extensions.Configuration;

namespace Abd.Shared.Utils.Configuration;

public static class ParsingExtensions
{
    public static bool IsTrue(this IConfiguration configuration, string value)
    {
        var boolStr = configuration[value];

        return !string.IsNullOrEmpty(value) &&
               bool.TryParse(boolStr, out var parsed) && parsed;
    }
}