using Microsoft.AspNetCore.Mvc;
using TransactionApi.Application.Dtos;
using TransactionApi.Application.Services.Abstract;
using TransactionApi.Infrasturcture.Services.Abstract;
using WalletService.Common.Services.Abstract;

namespace TransactionApi.Presentation.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IIdentityService _identityService;
        private readonly IQueueService _queueService;

        public TransactionController(ITransactionService transactionService, IIdentityService identityService, IQueueService queueService)
        {
            _transactionService = transactionService;
            _queueService = queueService;
            _identityService = identityService;
        }

         [HttpGet("{userId}/{walletId}")]
        public async Task<IActionResult> GetAllTransaction(string userId,int walletId)
        {
            if (userId != _identityService.GetUserId)
                return StatusCode(403, "Yetkiniz Yok");

            var list = await _transactionService.GetByUserIdAsync(_identityService.GetUserId, walletId);

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionCreateDto transaction)
        {

            if (transaction.UserId != _identityService.GetUserId)
                return StatusCode(403, "Yetkiniz Yok");

            var addedTransaction = await _transactionService.AddAsync(transaction);

            await _queueService.SendCreateTransactionEvent(new WalletService.Common.Messages.CreateTransactionCommand
            {
                Amount = addedTransaction.Amount,
                TransactionId = addedTransaction.Id,
                WalletId = addedTransaction.WalletId,
                UserId = _identityService.GetUserId
            });

            return Ok();
        }
    }
