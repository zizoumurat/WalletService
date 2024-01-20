using Moq;
using Shouldly;
using WalletApi.Application.Features.WalletFeatures.Commands.DeleteWallet;
using WalletApi.Domain.Entities;
using WalletApi.UnitTest.Fixture;

namespace WalletApi.UnitTest.Features.WalletFeatures.Commands;

public class DeleteWallendCommandUnitTest : IClassFixture<MockFixture>
{
    private readonly MockFixture _mockFixture;

    public DeleteWallendCommandUnitTest(MockFixture mockFixture)
    {
        _mockFixture = mockFixture;
    }

    [Fact]
    public async Task Handle_ShouldDeleteWalletSuccessfully()
    {
        var deleteWalletHandler = new DeleteWalletHandler(
            _mockFixture.WalletRepositoryMock.Object,
            _mockFixture.UnitOfWorkMock.Object
        );

        var deleteWalletCommand = new DeleteWalletCommand
        (
            walletId: 1,
            userId: Guid.NewGuid().ToString()
        );

        _mockFixture.WalletRepositoryMock.Setup(r => r.GetWalletByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new Wallet { Balance = 0 });

        _mockFixture.WalletRepositoryMock.Setup(r => r.DeleteWallet(It.IsAny<Wallet>())).Verifiable();

        _mockFixture.UnitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(_mockFixture.TransactionMock.Object);

        var result = await deleteWalletHandler.Handle(deleteWalletCommand, CancellationToken.None);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Handle_ShouldThrowExceptionOnNonExistingWallet()
    {
        var deleteWalletHandler = new DeleteWalletHandler(
            _mockFixture.WalletRepositoryMock.Object,
            _mockFixture.UnitOfWorkMock.Object
        );

        var deleteWalletCommand = new DeleteWalletCommand
        (
            walletId: 1,
            userId: Guid.NewGuid().ToString()
        );

        _mockFixture.WalletRepositoryMock.Setup(r => r.GetWalletByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync((Wallet)null); 

        await Should.ThrowAsync<Exception>(async () =>
            await deleteWalletHandler.Handle(deleteWalletCommand, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowExceptionOnNonZeroBalanceWallet()
    {
        var deleteWalletHandler = new DeleteWalletHandler(
            _mockFixture.WalletRepositoryMock.Object,
            _mockFixture.UnitOfWorkMock.Object
        );

        var deleteWalletCommand = new DeleteWalletCommand
        (
            walletId: 1,
            userId: Guid.NewGuid().ToString()
        );

        _mockFixture.WalletRepositoryMock.Setup(r => r.GetWalletByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new Wallet { Balance = 100 }); 
        
        await Should.ThrowAsync<Exception>(async () =>
            await deleteWalletHandler.Handle(deleteWalletCommand, CancellationToken.None));
    }
}
