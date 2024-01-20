using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Application.Messaging;
using WalletApi.Domain.Constants;

namespace WalletApi.Application.Features.WalletFeatures.Commands.DeleteWallet;

public class DeleteWalletHandler : ICommandHandler<DeleteWalletCommand, DeleteWalletResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteWalletHandler(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteWalletResponse> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetWalletByIdAsync(request.walletId, request.userId);

        if (wallet == null)
            throw new Exception(ErrorMessages.WalletNotFound);

        if (wallet.Balance > 0)
            throw new Exception(ErrorMessages.WalletCanNotDelete);

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                _walletRepository.DeleteWallet(wallet);
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