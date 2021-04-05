
using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class AppSettings
    {
        public string ApiCoutries { get; private set; }
        public string GitRespository { get; private set; }
        public string ConnectionStringDefault { get; private set; }
        public string ConnectionStringDocker { get; private set; }
        public string ClientId { get; private set; }
        public JWTSettings JWTSettings { get; private set; }
        
        public AppSettings(IConfiguration configuration)
        {
            ApiCoutries = configuration["ApiCoutries:EndPoint"];
            GitRespository = configuration["GitRespository:Url"];
            ConnectionStringDefault = configuration["ConnectionStrings:Default"];
            ConnectionStringDocker = configuration["ConnectionStrings:Docker"];
            ClientId = configuration.GetSection("Authentication:Google")["ClientId"];
            JWTSettings = new JWTSettings(configuration);
        }
    }
}