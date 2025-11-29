using MauiEnterpriseApp.Resources.Localization;

namespace MauiEnterpriseApp.ViewModels.Auth
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
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
                        // Teknik hatalar: network, server crash, vs.
                        // Ortak bir resource key kullanabiliriz
                        ErrorMessage = AppResources.Common_Error_Technical;
                    }
                    else
                    {
                        // İşlemsel hata: yanlış şifre, kullanıcı pasif, vs.
                        // Backend'ten gelen MessageCode'a göre mapping yapabiliriz
                        ErrorMessage = MapLoginErrorMessage(result.MessageCode);
                    }

                    return;
                }

                // TODO: Başarılı giriş → Shell navigation
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
