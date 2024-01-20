using WalletService.Common.Messages;

namespace TransactionApi.Infrasturcture.Services.Abstract;

public interface IQueueService
{
    Task SendCreateTransactionEvent(CreateTransactionCommand command);
}
