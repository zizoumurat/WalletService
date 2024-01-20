using MediatR;

namespace WalletApi.Application.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}