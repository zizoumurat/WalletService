using AutoMapper;
using Moq;
using Shouldly;
using WalletApi.Application.Contracts.Persistance.Repositories;
using WalletApi.Application.Dtos;
using WalletApi.Application.Features.WalletFeatures.Queries.GetAllWallet;
using WalletApi.Domain.Entities;
using WalletApi.UnitTest.Fixture;

namespace WalletApi.UnitTest.Features.WalletFeatures.Queries;

public class GetAllWalletQueryUnitTest : IClassFixture<MockFixture>
{
    private readonly MockFixture _mockFixture;

    public GetAllWalletQueryUnitTest(MockFixture mockFixture)
    {
        _mockFixture = mockFixture;
    }
    [Fact]
    public async Task Handle_ShouldReturnAllWalletsForUser()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var expectedWallets = new List<Wallet>
        {
            new Wallet { UserId = userId, Balance = 50, Name = "Wallet1", WalletCurrency = Domain.Enums.Currency.USD, CreatedDate = DateTime.Now.AddDays(-3)},
            new Wallet { UserId = userId, Balance = 120, Name = "Wallet2", WalletCurrency = Domain.Enums.Currency.TL, CreatedDate = DateTime.Now.AddDays(-1)},
        };

        _mockFixture.WalletRepositoryMock.Setup(r => r.GetUserWalletsAsync(userId))
            .ReturnsAsync(expectedWallets);


        _mockFixture.MapperMock.Setup(m => m.Map<IList<WalletDto>>(It.IsAny<List<Wallet>>()))
            .Returns((List<Wallet> wallets) =>
             wallets.Select(wallet => _mockFixture.MapperMock.Object.Map<WalletDto>(wallet)).ToList());

        var handler = new GetAllWalletHandler(_mockFixture.WalletRepositoryMock.Object, _mockFixture.MapperMock.Object);
        var query = new GetAllWalletQuery(userId);

        var response = await handler.Handle(query, CancellationToken.None);

        response.ShouldNotBeNull();
        response.walletList.ShouldNotBeNull();
        response.walletList.Count.Equals(expectedWallets.Count);
        
        _mockFixture.WalletRepositoryMock.Verify(r => r.GetUserWalletsAsync(userId), Times.Once);

        _mockFixture.MapperMock.Verify(m => m.Map<IList<WalletDto>>(expectedWallets), Times.Once);
    }
}
