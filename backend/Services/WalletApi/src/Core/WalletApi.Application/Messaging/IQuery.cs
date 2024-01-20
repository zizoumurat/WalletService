using MediatR;

namespace WalletApi.Application.Messaging;
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}