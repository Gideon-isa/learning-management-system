namespace Lms.Identity.Application.Features.Identity.Tokens
{
    public class TokenResponse
    {
        public string Jwt { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryDate { get; set; }
    }
}
