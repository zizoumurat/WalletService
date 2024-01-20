using TransactionApi.Application.Contracts.Persistance.Repositories;
using TransactionApi.Persistance;
using TransactionApi.Persistance.Context.Mongo;
using TransactionApi.Persistance.Repositories.Mongo;
using TransactionApi.WebApi.OptionSetups;
using WalletService.Common.Services;
using WalletService.Common.Services.Abstract;

namespace TransactionApi.WebApi.Configurations;

public class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<MongoOptionsSetup>();
        services.AddScoped<ITransactionDbContext, TransactionDbContext>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddAutoMapper(typeof(AssemblyReference).Assembly);

        services.AddScoped<IIdentityService, IdentityService>();
    }
}
