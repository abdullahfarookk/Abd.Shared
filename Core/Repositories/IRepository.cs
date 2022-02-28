namespace Abd.Shared.Core.Repositories;

public interface IRepository
{
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}