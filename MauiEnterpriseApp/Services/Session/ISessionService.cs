using MauiEnterpriseApp.Models.Auth;

namespace MauiEnterpriseApp.Services.Session
{
    /// <summary>
    /// Oturum bilgisini (login olmuş kullanıcı, token vb.) tutan servis sözleşmesi.
    /// Şimdilik sadece bellek içi çalışır, ileride kalıcı depolama eklenebilir.
    /// </summary>
    public interface ISessionService
    {
        LoginResponse? CurrentUser { get; }

        bool IsLoggedIn { get; }

        void SetLogin(LoginResponse response);

        void Clear();
    }
}
