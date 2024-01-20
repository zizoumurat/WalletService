using WalletApi.Domain.Enums;

namespace WalletApi.Domain.Entities;

public class Wallet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public decimal Balance { get; set; }
    public Currency WalletCurrency { get; set; }
    public DateTime CreatedDate { get; set; }
}
