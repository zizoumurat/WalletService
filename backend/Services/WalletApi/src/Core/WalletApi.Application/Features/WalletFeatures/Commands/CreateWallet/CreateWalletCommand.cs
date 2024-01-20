using WalletApi.Application.Messaging;
using WalletApi.Domain.Enums;

namespace WalletApi.Application.Features.WalletFeatures.Commands.CreateWallet;

public sealed record CreateWalletCommand(string name, Currency currency, string userId) : ICommand<CreateWalletResponse>;
