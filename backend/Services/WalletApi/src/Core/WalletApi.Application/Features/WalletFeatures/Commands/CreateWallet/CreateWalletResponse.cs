using WalletApi.Domain.Constants;

namespace WalletApi.Application.Features.WalletFeatures.Commands.CreateWallet;

public sealed record CreateWalletResponse(string message = ResponseMessages.WalletCreateSuccessMessage);
