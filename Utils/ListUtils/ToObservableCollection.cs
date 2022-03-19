using System.Collections.ObjectModel;

namespace Abd.Shared.Utils.ListUtils;

public static class ObservableCollectionExtension
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> value)
        => new(value);
}
