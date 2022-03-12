using System.Text.Json;

namespace Abd.Shared.Utils.JsonUtils;

public static class Converter
{
    public static string ToJson(this object value)
        => JsonSerializer.Serialize(value);
    public static T? ToObject<T>(this string value)
        => JsonSerializer.Deserialize<T>(value);
}