using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Domain.Entities;
using WalletApi.Persistance.Context;

namespace WalletApi.Persistance.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly WalletsDbContext _context;

    public WalletRepository(WalletsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Wallet>> GetUserWalletsAsync(string userId)
    {
        return await _context.Wallets.Where(w => w.UserId == userId).ToListAsync();
    }

    public async Task<Wallet> GetWalletAsync(Expression<Func<Wallet, bool>> filter)
    {
        return await _context.Wallets.FirstOrDefaultAsync(filter);
    }

    public async Task<Wallet> GetWalletByIdAsync(int walletId, string userId)
    {
        return await _context.Wallets.FirstOrDefaultAsync(x => x.Id == walletId && x.UserId == userId);
    }

    public void AddWallet(Wallet wallet)
    {
        _context.Wallets.Add(wallet);
    }

    public void DeleteWallet(Wallet wallet)
    {
        _context.Entry(wallet).State = EntityState.Deleted;
    }

    public void UpdateWallet(Wallet wallet)
    {
        _context.Entry(wallet).State = EntityState.Modified;
    }
}
