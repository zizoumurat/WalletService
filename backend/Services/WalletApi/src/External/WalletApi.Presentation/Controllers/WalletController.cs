using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WalletApi.Presentation.Abstraction;
using WalletApi.Application.Features.WalletFeatures.Queries.GetWallet;
using WalletApi.Application.Features.WalletFeatures.Queries.GetAllWallet;
using WalletService.Common.Services.Abstract;
using WalletApi.Application.Features.WalletFeatures.Commands.CreateWallet;
using WalletApi.Application.Features.WalletFeatures.Commands.DeleteWallet;


namespace WalletApi.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class WalletController : ApiController
    {
        public WalletController(IMediator mediator, IIdentityService identityService) : base(mediator, identityService)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWallet(int id)
        {
            GetWalletQuery request = new(userId: _identityService.GetUserId, walletId: id);

            var result = await _mediator.Send(request);

            return Ok(result.wallet);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWallet()
        {
            GetAllWalletQuery request = new(userId: _identityService.GetUserId);

            var result = await _mediator.Send(request);

            return Ok(result.walletList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet(CreateWalletCommand request)
        {

            if (request.userId != _identityService.GetUserId)
                return StatusCode(403, "Yetkiniz Yok");

            var result = await _mediator.Send(request);

            return Ok(result.message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            DeleteWalletCommand request = new(userId: _identityService.GetUserId, walletId: id);

            var result = await _mediator.Send(request);

            return Ok(result.message);
        }
    }
}
