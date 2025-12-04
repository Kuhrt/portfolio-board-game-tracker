using BoardGameTracker.Data.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGameTracker.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        SetAuditProperties(userId);

        int count = await _dbContext.SaveChangesAsync(cancellationToken);

        return count;
    }

    private void SetAuditProperties(Guid userId)
    {
        var entries = _dbContext.ChangeTracker.Entries<IAudit>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreated(userId);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdated(userId);
            }
        }
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}