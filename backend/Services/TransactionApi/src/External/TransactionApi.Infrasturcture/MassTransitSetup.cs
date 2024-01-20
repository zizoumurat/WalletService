using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionApi.Infrasturcture.Consumers;
using WalletService.Common.Constants;

namespace TransactionApi.Infrasturcture;

public static class MassTransitSetup
{
    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<TransactionComplatedConsumer>();
            x.AddConsumer<TransactionFailedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], host =>
                {
                    host.Username(configuration["RabbitMQ:UserName"]);
                    host.Password(configuration["RabbitMQ:Password"]);
                });

                cfg.ReceiveEndpoint(QueueNames.transactionComplatedQueueName, e =>
                {
                    e.ConfigureConsumer<TransactionComplatedConsumer>(context);
                });

                cfg.ReceiveEndpoint(QueueNames.transactionFailedQueueName, e =>
                {
                    e.ConfigureConsumer<TransactionFailedConsumer>(context);
                });

            });
        });

        services.AddHostedService<MassTransitHostedService>();

        return services;
    }
}

