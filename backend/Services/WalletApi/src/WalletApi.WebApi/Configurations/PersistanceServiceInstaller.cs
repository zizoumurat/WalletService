using Microsoft.EntityFrameworkCore;
using WalletApi.Persistance.Context;

namespace WalletApi.WebApi.Configurations;

public class PersistanceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WalletsDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        var dbContext = services.BuildServiceProvider().GetService<WalletsDbContext>();
        dbContext.Database.Migrate();
    }
}
