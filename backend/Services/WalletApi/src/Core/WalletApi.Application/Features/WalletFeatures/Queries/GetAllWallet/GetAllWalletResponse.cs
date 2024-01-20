using WalletApi.Application.Dtos;

namespace WalletApi.Application.Features.WalletFeatures.Queries.GetAllWallet;

public sealed record GetAllWalletResponse(IList<WalletDto> walletList);