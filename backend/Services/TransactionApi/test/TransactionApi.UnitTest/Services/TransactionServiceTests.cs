using Moq;
using Shouldly;
using TransactionApi.Application.Dtos;
using TransactionApi.Application.Services;
using TransactionApi.Domain.Entities;
using TransactionApi.UnitTest.Fixture;

namespace TransactionApi.UnitTest.Services;

public class TransactionServiceTests : IClassFixture<MockFixture>
{
    private readonly MockFixture _mockFixture;

    public TransactionServiceTests(MockFixture mockFixture)
    {
        _mockFixture = mockFixture;
    }

    [Fact]
    public async Task AddAsync_ShouldAddTransactionSuccessfully()
    {
        var transactionService = new TransactionService(
            _mockFixture.MockRepository.TransactionRepository.Object,
            _mockFixture.MockMapper.Mapper.Object
        );

        var inputDto = new TransactionDto();

        _mockFixture.MockMapper.Mapper.Setup(m => m.Map<Transaction>(It.IsAny<TransactionDto>()))
            .Returns(new Transaction());

        _mockFixture.MockMapper.Mapper.Setup(m => m.Map<TransactionDto>(It.IsAny<Transaction>()))
            .Returns(new TransactionDto()); 

        var result = await transactionService.AddAsync(inputDto);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTransactionById()
    {
        var transactionService = new TransactionService(
            _mockFixture.MockRepository.TransactionRepository.Object,
            _mockFixture.MockMapper.Mapper.Object
        );

        var transactionId = Guid.NewGuid().ToString();

        _mockFixture.MockRepository.TransactionRepository.Setup(r => r.GetByIdAsync(transactionId))
            .ReturnsAsync(new Transaction());

        _mockFixture.MockMapper.Mapper.Setup(m => m.Map<TransactionDto>(It.IsAny<Transaction>()))
            .Returns(new TransactionDto());

        var result = await transactionService.GetByIdAsync(transactionId);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnTransactionsByUserId()
    {
        var transactionService = new TransactionService(
            _mockFixture.MockRepository.TransactionRepository.Object,
            _mockFixture.MockMapper.Mapper.Object
        );

        var userId = Guid.NewGuid().ToString();

        _mockFixture.MockRepository.TransactionRepository.Setup(r => r.GetAllByUserId(userId))
            .ReturnsAsync(new List<Transaction>());

        _mockFixture.MockMapper.Mapper.Setup(m => m.Map<List<TransactionDto>>(It.IsAny<List<Transaction>>()))
            .Returns<List<Transaction>>(transactionList =>
                transactionList.Select(transaction => _mockFixture.MockMapper.Mapper.Object.Map<TransactionDto>(transaction)).ToList());

        var result = await transactionService.GetByUserIdAsync(userId);

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTransactionSuccessfully()
    {
        var transactionService = new TransactionService(
            _mockFixture.MockRepository.TransactionRepository.Object,
            _mockFixture.MockMapper.Mapper.Object
        );

        var inputDto = new TransactionDto();

        _mockFixture.MockMapper.Mapper.Setup(m => m.Map<Transaction>(It.IsAny<TransactionDto>()))
            .Returns(new Transaction());

        await transactionService.UpdateAsync(inputDto);
    }
}