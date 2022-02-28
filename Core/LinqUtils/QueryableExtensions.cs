

namespace Abd.Shared.Core.LinqUtils;

public static class QueryableExtensions
{
    private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Single(method =>
        method.Name == "OrderBy" && method.GetParameters().Length == 2);

    private static readonly MethodInfo OrderByDescendingMethod =
        typeof(Queryable).GetMethods().Single(method =>
            method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

    public static IQueryable<T> OrderByProperty<T>(
        this IQueryable<T> source, string propertyName)
    {
        if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                BindingFlags.Public | BindingFlags.Instance) == null)
        {
            return null!;
        }
        var parameterExpression = Expression.Parameter(typeof(T));
        Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
        var lambda = Expression.Lambda(orderByProperty, parameterExpression);
        var genericMethod =
            OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
        var ret = genericMethod.Invoke(null, new object[] { source, lambda });
        return (IQueryable<T>)ret!;
    }

    public static IQueryable<T> OrderByPropertyDescending<T>(
        this IQueryable<T> source, string propertyName)
    {
        if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                BindingFlags.Public | BindingFlags.Instance) == null)
        {
            return null!;
        }
        var paramterExpression = Expression.Parameter(typeof(T));
        var orderByProperty = Expression.Property(paramterExpression, propertyName);
        var lambda = Expression.Lambda(orderByProperty, paramterExpression);
        var genericMethod =
            OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
        var ret = genericMethod.Invoke(null, new object[] { source, lambda });
        return (IQueryable<T>)ret!;
    }
    public static IEnumerable<T> OrderByProperty<T>(this IEnumerable<T> entities, string propertyName)
    {
        var orderBy = entities as T[] ?? entities.ToArray();
        if (!orderBy.Any() || string.IsNullOrEmpty(propertyName))
            return orderBy;

        var propertyInfo = orderBy!.First()?.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        return orderBy.OrderBy(e => propertyInfo!.GetValue(e, null));
    }

    public static IEnumerable<T> OrderByPropertyDescending<T>(this IEnumerable<T> entities, string propertyName)
    {
        var orderBy = entities as T[] ?? entities.ToArray();
        if (!orderBy.Any() || string.IsNullOrEmpty(propertyName))
            return orderBy;

        var propertyInfo = orderBy.First()?.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        return orderBy.OrderByDescending(e => propertyInfo!.GetValue(e, null));
    }
    public static IQueryable<TEntity> GetPage<TEntity, TKey>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TKey>> orderByKeySelector, int pageNumber, int pageSize, bool sortByAsc = true)
    {

        if (pageSize < 1 || pageNumber < 1)
        {
            return queryable;
        }


        var result = predicate == null ? queryable : queryable.Where(predicate);
        if (sortByAsc)
        {
            result = result.OrderBy(orderByKeySelector);
        }
        else
        {
            result = result.OrderByDescending(orderByKeySelector);
        }

        return result.Skip(pageNumber * pageSize).Take(pageSize).Where(predicate!);
    }
}