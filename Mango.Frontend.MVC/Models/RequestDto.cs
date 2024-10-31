using Mango.Frontend.MVC.Enums;

namespace Mango.Frontend.MVC.Models
{
    public class RequestDto
    {
        public ApiTypeEnum HttpVerb { get; set; } = ApiTypeEnum.GET;
        public string Url { get; set; } = string.Empty;
        public object Data { get; set; } = default!;
        public string AccessToken { get; set; } = string.Empty;
    }
}
