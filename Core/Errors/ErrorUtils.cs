namespace Abd.Shared.Core.Errors;

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