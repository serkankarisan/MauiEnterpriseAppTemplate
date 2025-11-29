using MauiEnterpriseApp.Services.Auth;

public interface IAuthService
{
    Task<LoginServiceResult> LoginAsync(string email, string password);
}
