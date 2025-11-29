namespace MauiEnterpriseApp.Models.Auth
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
        // İleride RefreshToken, ExpiresAt vs. ekleyebilirsin.
    }
}
