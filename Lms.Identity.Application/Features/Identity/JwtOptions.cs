namespace Lms.Identity.Application.Features.Identity
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;
        public int TokenExpiryTimeInMinutes { get; set; }
        public int RefreshTokenExpiryTimeInDays { get; set; }
    }
}
