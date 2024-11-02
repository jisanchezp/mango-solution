using System.Text.Json;

namespace Mango.Frontend.MVC.Helper
{
    public static class JsonHelper
    {
        public static T? DeserializeCaseInsensitive<T>(string json)
        {
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
