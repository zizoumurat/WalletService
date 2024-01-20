using WalletApi.Domain.Constants;

namespace WalletApi.Application.Features.WalletFeatures.Commands.DeleteWallet;

public sealed record DeleteWalletResponse(string message = ResponseMessages.WalletDeleteSuccessMessage);
