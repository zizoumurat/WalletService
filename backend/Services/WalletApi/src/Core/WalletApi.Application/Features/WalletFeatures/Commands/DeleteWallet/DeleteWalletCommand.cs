using WalletApi.Application.Messaging;

namespace WalletApi.Application.Features.WalletFeatures.Commands.DeleteWallet;

public sealed record DeleteWalletCommand(string userId, int walletId) : ICommand<DeleteWalletResponse>;
