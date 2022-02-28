
#nullable enable
namespace Abd.Shared.Core.StreamUtils;

public static class StreamExtension
{
    public static bool IsStreamNull(this Stream? stream) =>
        stream is not { Length: > 0 };
}