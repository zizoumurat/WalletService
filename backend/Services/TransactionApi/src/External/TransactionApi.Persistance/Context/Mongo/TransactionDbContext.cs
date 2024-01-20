using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TransactionApi.Domain.Entities;
using TransactionApi.Persistance.Options;

namespace TransactionApi.Persistance.Context.Mongo;

public class TransactionDbContext : ITransactionDbContext
{
    private readonly IMongoDatabase _database;
    public TransactionDbContext(IOptions<MongoOptions> mongoSettings)
    {
        var settings = mongoSettings.Value;
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);

        Transactions = _database.GetCollection<Transaction>(settings.CollectionName);
    }
    public IMongoCollection<Transaction> Transactions { get; }
}
