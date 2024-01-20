using TransactionApi.Domain.Entities;

namespace TransactionApi.Persistance.Repositories.Abstract;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllByUserId(string userId);
    Task<Transaction> GetByIdAsync(string id);
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
}
