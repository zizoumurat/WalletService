using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TransactionApi.Application.Services.Abstract;
using TransactionApi.Domain.Constants;
using TransactionApi.Domain.Enums;
using TransactionApi.Infrasturcture.Hubs;
using WalletService.Common.Messages;

namespace TransactionApi.Infrasturcture.Consumers;

public class TransactionFailedConsumer : IConsumer<CreateTransactionFailedCommand>
{
    private readonly ITransactionService _transactionService;
    private readonly IHubContext<NotificationHub> _hubContext;

    public TransactionFailedConsumer(ITransactionService transactionService, IHubContext<NotificationHub> hubContext)
    {
        _transactionService = transactionService;
        _hubContext = hubContext;
    }
    public async Task Consume(ConsumeContext<CreateTransactionFailedCommand> context)
    {
        var transaction = await _transactionService.GetByIdAsync(context.Message.transactionId);

        transaction.Status = TransactionStatus.Failed;

        await _transactionService.UpdateAsync(transaction);
        await _hubContext.Clients.User(context.Message.userId).SendAsync("ReceiveNotification", new { Status = false, Message = ErrorMessages.SomethingWentWrong, ErrorMessage = context.Message.errorMessage });
    }
}
