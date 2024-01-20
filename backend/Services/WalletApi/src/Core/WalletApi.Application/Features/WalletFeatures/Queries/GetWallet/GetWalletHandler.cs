using AutoMapper;
using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Application.Dtos;
using WalletApi.Application.Messaging;

namespace WalletApi.Application.Features.WalletFeatures.Queries.GetWallet;

public class GetWalletHandler : IQueryHandler<GetWalletQuery, GetWalletResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public GetWalletHandler(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<GetWalletResponse> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var list = await _walletRepository.GetWalletByIdAsync(request.walletId, request.userId);

        var response = _mapper.Map<WalletDto>(list);

        return new(response);
    }
}
