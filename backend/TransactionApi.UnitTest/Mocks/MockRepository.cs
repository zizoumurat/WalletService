using Moq;
using TransactionApi.Application.Contracts.Persistance.Repositories;

namespace TransactionApi.UnitTest.Mocks;

public class MockRepository
{
    public Mock<ITransactionRepository> TransactionRepository { get; }

    public MockRepository()
    {
        TransactionRepository = new Mock<ITransactionRepository>();
    }
}
