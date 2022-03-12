namespace Abd.Shared.Utils.LinqUtils;

public static class SynList
{
    public static (List<TDto> add, List<TDb> remove, List<TUpdateResult> update) SyncWith<TDb, TDto, TSelector, TUpdateResult>(this List<TDb> dbs, List<TDto> dtos,
        Func<TDb, TSelector> dbSelector,
        Func<TDto, TSelector> dtoSelector,
        Func<TDb, TDto, TUpdateResult> update)
    {
        // add new items that not exist in db
        var dbItemsHash = new HashSet<TSelector>(dbs.Select(dbSelector));
        var itemsToAdd = dtos
            .Where(comparisonValue => !dbItemsHash.Contains(dtoSelector.Invoke(comparisonValue)))
            .ToList();

        // delete db Items that not exist in dto
        var dtoItemsHash = new HashSet<TSelector>(dtos.Select(dtoSelector));
        var ItemsToRemove = dbs
            .Where(comparisonValue => !dtoItemsHash.Contains(dbSelector.Invoke(comparisonValue)))
            .ToList();

        // update common Items
        var updates = dbs.Join(dtos, dbSelector, dtoSelector, update);
        return (itemsToAdd, ItemsToRemove, updates.ToList());
    }
}