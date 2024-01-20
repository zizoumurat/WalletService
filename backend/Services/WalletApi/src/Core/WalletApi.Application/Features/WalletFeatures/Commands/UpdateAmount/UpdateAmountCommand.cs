using WalletApi.Application.Messaging;

namespace WalletApi.Application.Features.WalletFeatures.Commands.UpdateAmount;

public sealed record UpdateAmountCommand(string userId, int walletId, decimal amount) : ICommand<UpdateAmountResponse>;
