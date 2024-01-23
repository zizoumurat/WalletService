using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Application.Messaging;
using WalletApi.Domain.Constants;

namespace WalletApi.Application.Features.WalletFeatures.Commands.UpdateAmount;

public class UpdateAmountHandler : ICommandHandler<UpdateAmountCommand, UpdateAmountResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAmountHandler(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateAmountResponse> Handle(UpdateAmountCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetWalletByIdAsync(request.walletId, request.userId);

        if (wallet == null)
            throw new Exception(ErrorMessages.WalletNotFound);

        if (request.amount < 0 && wallet.Balance < (request.amount * -1))
            throw new Exception(ErrorMessages.InsufficientBalance);

        wallet.Balance += request.amount;

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                _walletRepository.UpdateWallet(wallet);
                await _unitOfWork.CommitAsync();

                return new(request.amount > 0 ? ResponseMessages.DepositSuccessful : ResponseMessages.WithdrawalSuccessful);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();

                throw;
            }
        }
    }
}