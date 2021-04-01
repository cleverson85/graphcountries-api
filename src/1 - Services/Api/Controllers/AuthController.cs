using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthJwtService _authService;

        public AuthController(IAuthJwtService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuth externalAuth)
        {
            Payload payload = await _authService.VerifyGoogleToken(externalAuth);
            string token = await _authService.GenerateToken(payload);

            return Ok(new AuthResponse { Token = token, IsAuthenticaded = true });
        }
    }
}
