using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransactionApi.Domain.Enums;

namespace TransactionApi.Domain.Entities;

public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserId { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionStatus Status { get; set; }
}
