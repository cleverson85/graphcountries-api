using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Settings;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class AuthJwtService : IAuthJwtService
    {
        private readonly AppSettings _appSettings;
        public AuthJwtService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private async Task<SigningCredentials> GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.JWTSettings.SecurityKey);

            return await Task.FromResult(new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
        }

        private async Task<JwtSecurityToken> GenerateTokenOptions(SigningCredentials signingCredentials)
        {
            return await Task.FromResult
                (
                    new JwtSecurityToken(
                            issuer: _appSettings.JWTSettings.ValidIssuer,
                            audience: _appSettings.JWTSettings.ValidAudience,
                            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_appSettings.JWTSettings.ExpiryInMinutes)),
                            signingCredentials: signingCredentials
                        )
                );
        }

        public async Task<string> GenerateToken(GoogleJsonWebSignature.Payload payload)
        {
            var signingCredentials = await GetSigningCredentials();
            var tokenOptions = await GenerateTokenOptions(signingCredentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenOptions));
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuth externalAuth)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _appSettings.ClientId }
            };

            return await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
        }
    }
}


