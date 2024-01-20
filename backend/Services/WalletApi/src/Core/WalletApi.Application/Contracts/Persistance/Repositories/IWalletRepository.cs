using System.Linq.Expressions;
using WalletApi.Domain.Entities;

namespace WalletApi.Application.Contracts.Persistance.Repositories;

public interface IWalletRepository
{
    Task<Wallet> GetWalletByIdAsync(int walletId, string userId);
    Task<Wallet> GetWalletAsync(Expression<Func<Wallet, bool>> filter);
    Task<List<Wallet>> GetUserWalletsAsync(string userId);
    void AddWallet(Wallet wallet);
    void DeleteWallet(Wallet wallet);
    void UpdateWallet(Wallet wallet);
}
