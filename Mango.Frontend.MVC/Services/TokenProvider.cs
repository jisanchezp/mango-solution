using Mango.Frontend.MVC.Constants;
using Mango.Frontend.MVC.Services.Interfaces;

namespace Mango.Frontend.MVC.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(AuthConstants.TOKEN_COOKIE);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(AuthConstants.TOKEN_COOKIE, out token);

            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(AuthConstants.TOKEN_COOKIE, token);
        }
    }
}
