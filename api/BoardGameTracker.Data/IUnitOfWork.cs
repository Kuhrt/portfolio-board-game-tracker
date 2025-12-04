namespace BoardGameTracker.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken = default);
}