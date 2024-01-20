using TransactionApi.Application;
using TransactionApi.Application.Services;
using TransactionApi.Application.Services.Abstract;

namespace TransactionApi.WebApi.Configurations;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AssemblyReference).Assembly);

        services.AddScoped<ITransactionService, TransactionService>();
    }
}
