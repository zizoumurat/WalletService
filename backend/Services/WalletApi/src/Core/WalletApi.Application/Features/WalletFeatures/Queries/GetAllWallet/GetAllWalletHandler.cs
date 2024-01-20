using AutoMapper;
using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Application.Dtos;
using WalletApi.Application.Messaging;

namespace WalletApi.Application.Features.WalletFeatures.Queries.GetAllWallet;

public class GetAllWalletHandler : IQueryHandler<GetAllWalletQuery, GetAllWalletResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public GetAllWalletHandler(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<GetAllWalletResponse> Handle(GetAllWalletQuery request, CancellationToken cancellationToken)
    {
        var list = await _walletRepository.GetUserWalletsAsync(request.userId);

        var response = _mapper.Map<IList<WalletDto>>(list);

        return new(response);
    }
}

