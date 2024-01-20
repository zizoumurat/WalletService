using MassTransit;
using TransactionApi.Infrasturcture.Services.Abstract;
using WalletService.Common.Constants;
using WalletService.Common.Messages;

namespace TransactionApi.Infrasturcture.Services;

public class QueueService : IQueueService
{
    private readonly IBusControl _busControl;

    public QueueService(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task SendCreateTransactionEvent(CreateTransactionCommand command)
    {
        var endpoint = await _busControl.GetSendEndpoint(new Uri(QueueNames.transactionQueueNameUri));
        await endpoint.Send(command);
    }
}
