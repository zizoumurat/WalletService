using WalletService.Common.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WalletService.Common.Services;

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
