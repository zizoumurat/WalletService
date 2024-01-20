using MongoDB.Driver;
using TransactionApi.Application.Contracts.Persistance.Repositories;
using TransactionApi.Domain.Entities;
using TransactionApi.Persistance.Context.Mongo;

namespace TransactionApi.Persistance.Repositories.Mongo;

public class TransactionRepository : ITransactionRepository
{
    private readonly ITransactionDbContext _dbContext;
    private readonly IMongoCollection<Transaction> _transactionCollection;

    public TransactionRepository(ITransactionDbContext dbContext)
    {
         _dbContext = dbContext;
        _transactionCollection = _dbContext.Transactions;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _transactionCollection.InsertOneAsync(transaction);
    }

    public async Task<IEnumerable<Transaction>> GetAllByUserId(string userId)
    {
        return await _transactionCollection.Find(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Transaction> GetByIdAsync(string id)
    {
        return await _transactionCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        await _transactionCollection.ReplaceOneAsync(t => t.Id == transaction.Id, transaction);
    }
}
