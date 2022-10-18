namespace Abd.Shared.Utils.ObjectUtils;

public static class PropertySetter
{
    public static T SetProperty<T>(this T obj, string property, dynamic value)
    {
        obj?.GetType().BaseType?.GetProperty(property)?
            .SetValue(obj, value, null);
        return obj;
    }
}