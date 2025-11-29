using MauiEnterpriseApp.Models.Auth;

namespace MauiEnterpriseApp.Services.Session
{
    /// <summary>
    /// Basit bellek içi session servisi.
    /// İleride SecureStorage, Preferences vb. ile kalıcı hale getirilebilir.
    /// </summary>
    public class SessionService : ISessionService
    {
        private LoginResponse? _currentUser;

        public LoginResponse? CurrentUser => _currentUser;

        public bool IsLoggedIn => _currentUser != null;

        public void SetLogin(LoginResponse response)
        {
            _currentUser = response;
        }

        public void Clear()
        {
            _currentUser = null;
        }
    }
}
