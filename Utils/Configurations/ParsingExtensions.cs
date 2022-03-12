using Abd.Shared.Utils.StringUtils;
using Microsoft.Extensions.Configuration;

namespace Abd.Shared.Utils.Configurations;

public static class ParsingExtensions
{
    public static bool ParseBool(this IConfiguration configuration, string value)
    {
        var boolStr = configuration[value];

        return !boolStr.IsNullOrEmpty() &&
               bool.TryParse(boolStr, out var parsed) && parsed;
    }
}