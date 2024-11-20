namespace Mango.Services.AuthAPI.Models
{
    internal class JwtOptions
    {
        internal string Issuer { get; set; } = string.Empty;
        internal string Audience { get; set; } = string.Empty;
        internal string Secret { get; set; } = string.Empty;
    }
}
