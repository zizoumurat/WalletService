using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Application.Messaging;
using WalletApi.Domain.Constants;
using WalletApi.Domain.Entities;

namespace WalletApi.Application.Features.WalletFeatures.Commands.CreateWallet;

public class CreateWalletHandler : ICommandHandler<CreateWalletCommand, CreateWalletResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWalletHandler(IWalletRepository walletRepository, IUnitOfWork unitOfWork = null)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateWalletResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var existingWallet = await _walletRepository.GetWalletAsync(x => x.UserId == request.userId && x.Name == request.name.Trim());

        if (existingWallet != null)
            throw new Exception(ErrorMessages.WalletNameAlReadyExist);

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var wallet = new Wallet
                {
                    Name = request.name.Trim(),
                    WalletCurrency = request.currency,
                    UserId = request.userId,
                    Balance = 0,
                    CreatedDate = DateTime.Now.Date
                };

                _walletRepository.AddWallet(wallet);

                await _unitOfWork.CommitAsync();

                return new();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();

                throw;
            }
        }
    }
}