
#nullable enable
namespace Abd.Shared.Utils.StreamUtils;

public static class StreamExtension
{
    public static bool IsStreamNull(this Stream? stream) =>
        stream is not { Length: > 0 };
}