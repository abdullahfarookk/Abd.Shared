using System.Collections;

namespace Abd.Shared.Utils.ArrayUtils;

public static class NullOrZero
{
    public static bool IsNullOrZero(this IEnumerable? @this) => @this is null || !@this.GetEnumerator().MoveNext();
}