using Microsoft.EntityFrameworkCore.Storage;

namespace WalletApi.Application.Contracts.Persistance.Repositories;

public interface IUnitOfWork : IDisposable
{
    IDbContextTransaction BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
}
