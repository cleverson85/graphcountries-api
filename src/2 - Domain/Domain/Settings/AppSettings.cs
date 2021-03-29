using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class AppSettings : SettingsBase
    {
        public string END_POINT { get; private set; }
        public string CONNECTION_STRING { get; private set; }
        public string GIT_PROJECT { get; private set; }

        public AppSettings(IConfiguration configuration)
        {
            END_POINT = $"https://countries-274616.ew.r.appspot.com/";
            GIT_PROJECT = "";
            CONNECTION_STRING = $"Host=localhost;Port=15432;Pooling=true;Database=Countries;User Id=postgres;Password=docker;";
           
        }
    }
}
