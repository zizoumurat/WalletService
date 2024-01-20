using Moq;
using Shouldly;
using System.Linq.Expressions;
using WalletApi.Application.Features.WalletFeatures.Commands.CreateWallet;
using WalletApi.Domain.Entities;
using WalletApi.Domain.Enums;
using WalletApi.UnitTest.Fixture;

namespace WalletApi.UnitTest.Features.WalletFeatures.Commands;

public class CreateWallendCommandUnitTest : IClassFixture<MockFixture>
{
    private readonly MockFixture _mockFixture;

    public CreateWallendCommandUnitTest(MockFixture mockFixture)
    {
        _mockFixture = mockFixture;
    }

    [Fact]
    public async Task Handle_ShouldCreateWalletSuccessfully()
    {
        // Arrange
        var createWalletHandler = new CreateWalletHandler(
            _mockFixture.WalletRepositoryMock.Object,
            _mockFixture.UnitOfWorkMock.Object
        );

        var createWalletCommand = new CreateWalletCommand(userId: Guid.NewGuid().ToString(), name: "WalletName", currency: Currency.USD);

        _mockFixture.WalletRepositoryMock.Setup(r => r.GetWalletAsync(It.IsAny<Expression<Func<Wallet, bool>>>()))
            .ReturnsAsync((Wallet)null);

        _mockFixture.WalletRepositoryMock.Setup(r => r.AddWallet(It.IsAny<Wallet>())).Verifiable();

        _mockFixture.UnitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(_mockFixture.TransactionMock.Object);

        var result = await createWalletHandler.Handle(createWalletCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        _mockFixture.WalletRepositoryMock.Verify(r => r.GetWalletAsync(It.IsAny<Expression<Func<Wallet, bool>>>()), Times.Once);
        _mockFixture.WalletRepositoryMock.Verify(r => r.AddWallet(It.IsAny<Wallet>()), Times.Once);
        _mockFixture.UnitOfWorkMock.Verify(u => u.BeginTransaction(), Times.Once);
        _mockFixture.UnitOfWorkMock.Verify(t => t.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowExceptionOnExistingWallet()
    {
        var createWalletHandler = new CreateWalletHandler(
            _mockFixture.WalletRepositoryMock.Object,
            _mockFixture.UnitOfWorkMock.Object
        );

        var createWalletCommand = new CreateWalletCommand(userId: Guid.NewGuid().ToString(), name: "WalletName", currency: Currency.USD);

        _mockFixture.WalletRepositoryMock.Setup(r => r.GetWalletAsync(It.IsAny<Expression<Func<Wallet, bool>>>()))
            .ReturnsAsync(new Wallet());

        await Should.ThrowAsync<Exception>(async () =>
            await createWalletHandler.Handle(createWalletCommand, CancellationToken.None));

        _mockFixture.WalletRepositoryMock.Verify(r => r.AddWallet(It.IsAny<Wallet>()));
        _mockFixture.UnitOfWorkMock.Verify(u => u.BeginTransaction());
        _mockFixture.UnitOfWorkMock.Verify(t => t.CommitAsync());
    }
}
