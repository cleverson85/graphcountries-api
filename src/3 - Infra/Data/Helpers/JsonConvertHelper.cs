using Newtonsoft.Json;
using System.Text.Json;

namespace Data.Helpers
{
    public static class JsonConvertHelper
    {
        public static string SerializeObject<Entity>(Entity entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static Entity DeserializeObject<Entity>(string json) 
        {
            return JsonConvert.DeserializeObject<Entity>(json);
        }
    }
}
