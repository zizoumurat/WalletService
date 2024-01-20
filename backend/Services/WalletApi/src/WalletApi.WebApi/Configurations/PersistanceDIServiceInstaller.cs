using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Persistance.Repositories;
using WalletService.Common.Services;
using WalletService.Common.Services.Abstract;

namespace WalletApi.WebApi.Configurations;

public class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        #region UnitOfWork
        #endregion

        #region Repositories
        services.AddScoped<IWalletRepository, WalletRepository>();
        #endregion

        services.AddScoped<IIdentityService, IdentityService>();
    }
}
