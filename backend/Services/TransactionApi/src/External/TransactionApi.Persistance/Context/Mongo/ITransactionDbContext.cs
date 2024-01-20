using MongoDB.Driver;
using TransactionApi.Domain.Entities;

namespace TransactionApi.Persistance.Context.Mongo;

public interface ITransactionDbContext
{
    IMongoCollection<Transaction> Transactions { get; }
}
