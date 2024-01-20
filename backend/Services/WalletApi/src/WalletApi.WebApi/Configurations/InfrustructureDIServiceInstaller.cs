using WalletApi.Infrasturcture;
using WalletApi.Infrasturcture.Services;
using WalletApi.Infrasturcture.Services.Abstract;

namespace WalletApi.WebApi.Configurations;

public class InfrustructureDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddMassTransitWithRabbitMQ(configuration);
        services.AddScoped<IQueueService, QueueService>();
    }
}
