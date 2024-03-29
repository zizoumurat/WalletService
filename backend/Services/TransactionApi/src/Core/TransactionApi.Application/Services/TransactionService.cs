﻿using AutoMapper;
using TransactionApi.Application.Contracts.Persistance.Repositories;
using TransactionApi.Application.Dtos;
using TransactionApi.Application.Services.Abstract;
using TransactionApi.Domain.Entities;

namespace TransactionApi.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }
    public async Task<TransactionDto> AddAsync(TransactionCreateDto transaction)
    {
        var transactionEntity = _mapper.Map<Transaction>(transaction);
        transactionEntity.Status = Domain.Enums.TransactionStatus.Pending;
        transactionEntity.Date = DateTime.Now;

        await _transactionRepository.AddAsync(transactionEntity);

        return _mapper.Map<TransactionDto>(transactionEntity);
    }

    public async Task<TransactionDto> GetByIdAsync(string id)
    {
        var transactionEntity = await _transactionRepository.GetByIdAsync(id);

        return _mapper.Map<TransactionDto>(transactionEntity);
    }

    public async Task<IEnumerable<TransactionDto>> GetByUserIdAsync(string userId, int walletId)
    {
        var transactionList = await _transactionRepository.GetAllByUserId(userId, walletId);

        return _mapper.Map<List<TransactionDto>>(transactionList);
    }

    public async Task UpdateAsync(TransactionDto transaction)
    {
        var transactionEntity = _mapper.Map<Transaction>(transaction);
        await _transactionRepository.UpdateAsync(transactionEntity);
    }
}
