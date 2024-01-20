using WalletService.Common.Messages;

namespace WalletApi.Infrasturcture.Services.Abstract;

public interface IQueueService
{
    Task SendComplatedEvent(CreateTransactionComplatedCommand command);
    Task SendFailedCommand(CreateTransactionFailedCommand command);
}
