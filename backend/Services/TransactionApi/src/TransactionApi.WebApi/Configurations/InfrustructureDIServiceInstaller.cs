using TransactionApi.Infrasturcture;
using TransactionApi.Infrasturcture.Services;
using TransactionApi.Infrasturcture.Services.Abstract;

namespace TransactionApi.WebApi.Configurations;

public class InfrustructureDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddMassTransitWithRabbitMQ(configuration);
        services.AddScoped<IQueueService, QueueService>();
    }
}
