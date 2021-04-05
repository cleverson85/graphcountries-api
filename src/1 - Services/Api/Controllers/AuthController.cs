using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Domain.Util.Endpoints;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthJwtService _authService;

        public AuthController(IAuthJwtService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route(Route.POST)]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuth externalAuth)
        {
            Payload payload = await _authService.VerifyGoogleToken(externalAuth);
            string token = await _authService.GenerateToken(payload);

            return Ok(new AuthResponse { Token = token, IsAuthenticaded = true });
        }

        [HttpPost]
        [Route(Route.POST)]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            string token = await _authService.GenerateToken(user);

            if (token == null)
            {
                return BadRequest();
            }

            return Ok(new AuthResponse { Token = token, IsAuthenticaded = true, TempUser = true });
        }
    }
}
