#if NET6_0
using System.Runtime.CompilerServices;

namespace Abd.Shared.Core.StringUtils;

public static class EnsureThat
{
    public static void ItIsTrue(bool condition,
        [InterpolatedStringHandlerArgument("condition")]ref EnsureInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")] string paramName = "")
    {
        if (!condition)
        {
            throw new ArgumentNullException(paramName,message.ToString());
        }
    }
}
#endif
