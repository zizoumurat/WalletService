using Microsoft.EntityFrameworkCore.Storage;
using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Persistance.Context;

namespace WalletApi.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly WalletsDbContext _dbContext;
    private IDbContextTransaction _transaction;

    public UnitOfWork(WalletsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
        await _transaction?.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }
}
