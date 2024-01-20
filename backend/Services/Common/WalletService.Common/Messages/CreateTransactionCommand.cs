namespace WalletService.Common.Messages;

public class CreateTransactionCommand
{
    public string TransactionId { get; set; }
    public string UserId { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
}
