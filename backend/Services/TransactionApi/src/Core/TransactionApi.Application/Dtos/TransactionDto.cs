using TransactionApi.Domain.Enums;

namespace TransactionApi.Application.Dtos;

public sealed class TransactionDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionStatus Status { get; set; }
}
