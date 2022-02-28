namespace Abd.Shared.Core.Configurations;

public static class ParsingExtensions
{
    public static bool ParseBool(this IConfiguration configuration, string value)
    {
        var boolStr = configuration[value];

        return !boolStr.IsNullOrEmpty() &&
               bool.TryParse(boolStr, out var parsed) && parsed;
    }
}