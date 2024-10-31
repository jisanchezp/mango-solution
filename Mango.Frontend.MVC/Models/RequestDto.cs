using Mango.Frontend.MVC.Enums;

namespace Mango.Frontend.MVC.Models
{
    public class RequestDto
    {
        public HttpVerbEnum HttpVerb { get; set; } = HttpVerbEnum.GET;
        public string Url { get; set; } = string.Empty;
        public object DiscountAmount { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
