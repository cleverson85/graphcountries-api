using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class JWTSettings
    {
        public string SecurityKey { get; private set; }
        public string ValidIssuer { get; private set; }
        public string ValidAudience { get; private set; }
        public string ExpiryInMinutes { get; private set; }

        public JWTSettings(IConfiguration configuration)
        {
            SecurityKey = configuration["JWTSettings:SecurityKey"];
            ValidIssuer = configuration["JWTSettings:ValidIssuer"];
            ValidAudience = configuration["JWTSettings:ValidAudience"];
            ExpiryInMinutes = configuration["JWTSettings:ExpiryInMinutes"];
        }
    }
}
