namespace Abd.Shared.Core.Utils;

public static class TypeExtensions
{
    public static bool Implements<T>(this object obj, T @abstract) where T:class
    {
        if(@abstract is not Type type) 
            throw new ArgumentException("Only 'Type' are allowed.");
        return type.IsInstanceOfType(obj);
    }
    public static bool Implements<T>(this Type obj, T @abstract) where T:class
    {
        if(@abstract is not Type type) 
            throw new ArgumentException("Only 'Type' are allowed.");
        return type.IsInstanceOfType(obj);
    }
}