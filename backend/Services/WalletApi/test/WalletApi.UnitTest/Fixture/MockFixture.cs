using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using WalletApi.Application.Contracts.Persistance.Repositories;

namespace WalletApi.UnitTest.Fixture;

public class MockFixture
{
    public Mock<IWalletRepository> WalletRepositoryMock { get; }
    public Mock<IUnitOfWork> UnitOfWorkMock { get; }
    public Mock<IDbContextTransaction> TransactionMock { get; }
    public Mock<IMapper> MapperMock { get; }

    public MockFixture()
    {
        WalletRepositoryMock = new Mock<IWalletRepository>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();
        TransactionMock = new Mock<IDbContextTransaction>();
        MapperMock = new Mock<IMapper>();
    }
}