using TransactionApi.Domain.Entities;

namespace TransactionApi.Application.Contracts.Persistance.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllByUserId(string userId, int walletId);
    Task<Transaction> GetByIdAsync(string id);
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
}