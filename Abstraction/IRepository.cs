namespace Abd.Shared.Abstraction;

public interface IRepository
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}