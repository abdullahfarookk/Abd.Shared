using Abd.Shared.Core.Errors;
using Abd.Shared.Utils.StringUtils;

namespace Abd.Shared.Utils.ErrorUtils;

public static class ErrorUtils
{
    public static T Convert<T>(ResultError error, string? message = null)
    {
        var ctors = error.ErrorType.GetConstructors();
        return (T)ctors[0].Invoke(new object[] { (message.IsNullOrEmpty() ? error.Message : message)! });

    }
    public static T ConvertTo<T>(this ResultError error, string? message = null)
        => Convert<T>(error, message);
}