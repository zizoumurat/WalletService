using TransactionApi.Domain.Enums;

namespace TransactionApi.Application.Dtos;

public sealed class TransactionCreateDto
{
    public string UserId { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
}
