using System.Collections;

namespace Abd.Shared.Core.Utils;

public static class ObjectCreationExt
{
    public static T? CreateInstanceFrom<T>(this object? obj) where T : class
    {
        try
        {
            return Activator.CreateInstance(typeof(T), obj) as T;
        }
        catch (MissingMethodException e)
        {
            throw new MissingMethodException($"The constructor of '{typeof(T).Name}' does not have parameter of type '{obj?.GetType().Name}'", e);
        }
    }
    public static T? CreateInstanceFrom<T>(this IEnumerable<object?>? param) where T : class
    {
        try
        {
            return Activator.CreateInstance(typeof(T), param) as T;
        }
        catch (MissingMethodException e)
        {
            // get type names from obj and join them with comma
            var typeNames = string.Join(",", param!
                .Select(x => x is IEnumerable enu
                    ? $@"List<{enu.Cast<object>()
                        .FirstOrDefault()?
                        .GetType().Name}>"
                    : x.GetType().Name));
            throw new MissingMethodException($"The constructor of '{typeof(T).Name}' does not have parameter of types '{typeNames}'", e);
        }
    }
}