namespace Abd.Shared.Core.LinqUtils;

public static class OrderByProp
{
    private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Single(method =>
        method.Name == "OrderBy" && method.GetParameters().Length == 2);

    private static readonly MethodInfo OrderByDescendingMethod =
        typeof(Queryable).GetMethods().Single(method =>
            method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

    public static bool PropertyExists<T>(string propertyName)
    {
        return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                   BindingFlags.Public | BindingFlags.Instance) != null;
    }

    public static IQueryable<T> OrderByProperty<T>(
        this IQueryable<T> source, string propertyName)
    {
        if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                BindingFlags.Public | BindingFlags.Instance) == null)
        {
            return null;
        }
        var parameterExpression = Expression.Parameter(typeof(T));
        Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
        var lambda = Expression.Lambda(orderByProperty, parameterExpression);
        var genericMethod =
            OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
        var ret = genericMethod.Invoke(null, new object[] { source, lambda });
        return (IQueryable<T>)ret;
    }

    public static IQueryable<T> OrderByPropertyDescending<T>(
        this IQueryable<T> source, string propertyName)
    {
        if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                BindingFlags.Public | BindingFlags.Instance) == null)
        {
            return null;
        }
        var parameterExpression = Expression.Parameter(typeof(T));
        var orderByProperty = Expression.Property(parameterExpression, propertyName);
        var lambda = Expression.Lambda(orderByProperty, parameterExpression);
        var genericMethod =
            OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
        var ret = genericMethod.Invoke(null, new object[] { source, lambda });
        return (IQueryable<T>)ret;
    }
    public static IEnumerable<T> OrderByProperty<T>(this IEnumerable<T> entities, string propertyName)
    {
        var orderBy = entities as T[] ?? entities.ToArray();
        if (!orderBy.Any() || string.IsNullOrEmpty(propertyName))
            return orderBy;

        var propertyInfo = orderBy.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        return orderBy.OrderBy(e => propertyInfo!.GetValue(e, null));
    }

    public static IEnumerable<T> OrderByPropertyDescending<T>(this IEnumerable<T> entities, string propertyName)
    {
        var orderBy = entities as T[] ?? entities.ToArray();
        if (!orderBy.Any() || string.IsNullOrEmpty(propertyName))
            return orderBy;

        var propertyInfo = orderBy.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        return orderBy.OrderByDescending(e => propertyInfo!.GetValue(e, null));
    }

}