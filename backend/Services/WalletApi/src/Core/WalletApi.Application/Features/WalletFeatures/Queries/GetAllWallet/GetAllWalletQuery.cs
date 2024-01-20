using WalletApi.Application.Messaging;

namespace WalletApi.Application.Features.WalletFeatures.Queries.GetAllWallet;

public sealed record GetAllWalletQuery(string userId) : IQuery<GetAllWalletResponse>;

