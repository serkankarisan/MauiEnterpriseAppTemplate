using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Session;

namespace MauiEnterpriseApp.ViewModels.Auth
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        public LoginViewModel(IAuthService authService, ISessionService sessionService)
        {
            _authService = authService;
            _sessionService = sessionService;

            Title = AppResources.Login_Title;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = AppResources.Login_Error_Required;
                return;
            }

            try
            {
                IsBusy = true;

                var result = await _authService.LoginAsync(Email, Password);

                if (!result.IsSuccess)
                {
                    if (result.IsTechnicalError)
                    {
                        ErrorMessage = AppResources.Common_Error_Technical;
                    }
                    else
                    {
                        ErrorMessage = MapLoginErrorMessage(result.MessageCode);
                    }

                    return;
                }

                // Başarılı login ise, session'ı set edelim
                if (result.Data != null)
                {
                    _sessionService.SetLogin(result.Data);
                }
                // Dashboard'u root olarak aç (login ekranını stack'ten temizler)
                await Shell.Current.GoToAsync("//DashboardPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private string MapLoginErrorMessage(string? messageCode)
        {
            // Burada backend MessageCode → AppResources key mapping yapıyoruz.
            // Örnekler:
            return messageCode switch
            {
                "LOGIN_INVALID_CREDENTIALS" => AppResources.Login_Error_Invalid,
                "LOGIN_USER_LOCKED" => AppResources.Login_Error_Locked, // bu key'i de AppResources'e eklersin
                _ => AppResources.Common_Error_Generic
            };
        }
    }
}
