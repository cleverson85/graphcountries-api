using Domain.Models;
using Google.Apis.Auth;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAuthJwtService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuth externalAuth);
        Task<string> GenerateToken(GoogleJsonWebSignature.Payload payload);
    }
}
