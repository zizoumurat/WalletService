using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TransactionApi.Application.Services.Abstract;
using TransactionApi.Domain.Constants;
using TransactionApi.Domain.Enums;
using TransactionApi.Infrasturcture.Hubs;
using WalletService.Common.Messages;

namespace TransactionApi.Infrasturcture.Consumers;

public class TransactionComplatedConsumer : IConsumer<CreateTransactionComplatedCommand>
{
    private readonly ITransactionService _transactionService;
    private readonly IHubContext<NotificationHub> _hubContext;

    public TransactionComplatedConsumer(ITransactionService transactionService, IHubContext<NotificationHub> hubContext)
    {
        _transactionService = transactionService;
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<CreateTransactionComplatedCommand> context)
    {
        var transaction = await _transactionService.GetByIdAsync(context.Message.transactionId);

        transaction.Status = TransactionStatus.Complated;

        await _transactionService.UpdateAsync(transaction);

        await _hubContext.Clients.User(context.Message.userId).SendAsync("ReceiveNotification", new { Status = true, Message = ResponseMessages.TransactionComplated});
    }
}
