using WalletApi.Application.Dtos;

namespace WalletApi.Application.Features.WalletFeatures.Queries.GetWallet;

public sealed record GetWalletResponse(WalletDto wallet);