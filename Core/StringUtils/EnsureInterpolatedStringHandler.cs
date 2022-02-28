using System.Runtime.CompilerServices;

namespace Abd.Shared.Core.StringUtils;

[InterpolatedStringHandler]
public ref struct EnsureInterpolatedStringHandler
{
    private DefaultInterpolatedStringHandler _innerHandler;
    public EnsureInterpolatedStringHandler(int literalLength, int formattedCount, bool condition, out bool shouldAppend)
    {
        if (condition is true)
        {
            _innerHandler = default;
            shouldAppend = false;
            return;
        }
        _innerHandler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }
    public override string ToString() =>_innerHandler.ToString();
    public string ToStringAndClear() => _innerHandler.ToStringAndClear();
    public void AppendLiteral(string message)
    {
        _innerHandler.AppendLiteral(message);
    }
    public void AppendFormatted<T>(T message)
    {
        _innerHandler.AppendFormatted(message);
    }
}