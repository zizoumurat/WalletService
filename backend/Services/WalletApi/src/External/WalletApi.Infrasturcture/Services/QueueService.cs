using MassTransit;
using WalletApi.Infrasturcture.Services.Abstract;
using WalletService.Common.Constants;
using WalletService.Common.Messages;

namespace WalletApi.Infrasturcture.Services;

public class QueueService : IQueueService
{
    private readonly IBusControl _busControl;

    public QueueService(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task SendComplatedEvent(CreateTransactionComplatedCommand command)
    {
        var endpoint = await _busControl.GetSendEndpoint(new Uri(QueueNames.transactionComplatedQueueNameUri));
        await endpoint.Send(command);
    }

    public async Task SendFailedCommand(CreateTransactionFailedCommand command)
    {
        var endpoint = await _busControl.GetSendEndpoint(new Uri(QueueNames.transactionFailedQueueNameUri));
        await endpoint.Send(command);
    }
}
