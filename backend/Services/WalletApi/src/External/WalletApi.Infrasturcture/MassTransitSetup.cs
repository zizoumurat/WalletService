using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletApi.Infrasturcture.Consumers;
using WalletService.Common.Constants;

namespace WalletApi.Infrasturcture;

public static class MassTransitSetup
{
    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateTransactionConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], host =>
                {
                    host.Username(configuration["RabbitMQ:UserName"]);
                    host.Password(configuration["RabbitMQ:Password"]);
                });

                cfg.ReceiveEndpoint(QueueNames.transactionQueueName, e =>
                {
                    e.ConfigureConsumer<CreateTransactionConsumer>(context);
                });

            });
        });

        services.AddHostedService<MassTransitHostedService>();

        return services;
    }
}
