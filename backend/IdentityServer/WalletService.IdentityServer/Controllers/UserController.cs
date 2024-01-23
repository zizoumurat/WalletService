using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WalletService.IdentityServer.Models;
using static IdentityServer4.IdentityServerConstants;

namespace WalletService.IdentityServer.Controllers
{

    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return BadRequest();
            return Ok(new { user.Id, user.Name, user.LastName });
        }
    }
}
