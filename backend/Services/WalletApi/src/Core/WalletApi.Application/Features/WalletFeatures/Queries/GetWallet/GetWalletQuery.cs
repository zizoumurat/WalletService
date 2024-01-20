using WalletApi.Application.Messaging;

namespace WalletApi.Application.Features.WalletFeatures.Queries.GetWallet;

public sealed record GetWalletQuery(string userId, int walletId) : IQuery<GetWalletResponse>;
