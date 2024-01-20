using MassTransit;
using MediatR;
using WalletApi.Application.Features.WalletFeatures.Commands.UpdateAmount;
using WalletApi.Domain.Constants;
using WalletApi.Infrasturcture.Services.Abstract;
using WalletService.Common.Messages;

namespace WalletApi.Infrasturcture.Consumers;

public class CreateTransactionConsumer : IConsumer<CreateTransactionCommand>
{
    private readonly IMediator _mediator;
    private readonly IQueueService _queueService;

    public CreateTransactionConsumer(IMediator mediator, IQueueService queueService)
    {
        _mediator = mediator;
        _queueService = queueService;
    }

    public async Task Consume(ConsumeContext<CreateTransactionCommand> context)
    {
        try
        {
            int walletId = context.Message.WalletId;
            string userId = context.Message.UserId;
            var amount = context.Message.Amount;

            if (amount == 999)
                throw new Exception(ErrorMessages.TryAgainLater);

            var request = new UpdateAmountCommand(userId, walletId, amount);
            await _mediator.Send(request);

            await _queueService.SendComplatedEvent(new(transactionId: context.Message.TransactionId, userId: context.Message.UserId));
        }
        catch (Exception ex)
        {
            await _queueService.SendFailedCommand(new(transactionId: context.Message.TransactionId, errorMessage: ex.Message, userId: context.Message.UserId));
        }
    }
}
