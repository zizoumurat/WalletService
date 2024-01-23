using TransactionApi.Application.Dtos;

namespace TransactionApi.Application.Services.Abstract;

public interface ITransactionService
{
    Task<TransactionDto> GetByIdAsync(string id);
    Task<IEnumerable<TransactionDto>> GetByUserIdAsync(string userId, int walletId);
    Task<TransactionDto> AddAsync(TransactionCreateDto transaction);
    Task UpdateAsync(TransactionDto transaction);
}
