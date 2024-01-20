using Microsoft.EntityFrameworkCore;
using WalletApi.Domain.Entities;

namespace WalletApi.Persistance.Context
{
    public class WalletsDbContext : DbContext
    {
        public WalletsDbContext(DbContextOptions<WalletsDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Wallet> Wallets { get; set; }
    }
}
