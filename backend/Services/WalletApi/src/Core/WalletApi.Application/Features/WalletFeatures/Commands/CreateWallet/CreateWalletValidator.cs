using FluentValidation;
using WalletApi.Domain.Constants;

namespace WalletApi.Application.Features.WalletFeatures.Commands.CreateWallet;

public sealed class CreateWalletValidator : AbstractValidator<CreateWalletCommand>
{
    public CreateWalletValidator()
    {
        RuleFor(r => r.name).NotNull().WithMessage(ValidationMessages.WalletNameIsRequired);
        RuleFor(r => r.currency).NotEmpty().WithMessage(ValidationMessages.WalletCurrencyIsRequired);
    }
}