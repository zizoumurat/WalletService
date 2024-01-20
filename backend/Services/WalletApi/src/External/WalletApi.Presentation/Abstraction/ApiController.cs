using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalletService.Common.Services.Abstract;

namespace WalletApi.Presentation.Abstraction;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly IIdentityService _identityService;

    protected ApiController(IMediator mediator, IIdentityService identityService)
    {
        _mediator = mediator;
        _identityService = identityService;
    }
}
