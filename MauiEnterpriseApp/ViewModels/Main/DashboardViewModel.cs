using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Session;

namespace MauiEnterpriseApp.ViewModels.Main
{
    /// <summary>
    /// Dashboard (ana sayfa) için ViewModel.
    /// Kullanıcının adını ve basit özet bilgileri gösterir.
    /// </summary>
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly ISessionService _sessionService;

        [ObservableProperty]
        private string? welcomeText = string.Empty;

        public DashboardViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;

            Title = AppResources.Dashboard_Title;

            Initialize();
        }

        private void Initialize()
        {
            var userName = _sessionService.CurrentUser?.UserName;

            if (string.IsNullOrWhiteSpace(userName))
            {
                // Kullanıcı adı yoksa daha genel bir mesaj
                WelcomeText = AppResources.Dashboard_Welcome_Generic;
            }
            else
            {
                // "Hoş geldin, {0}" tarzı resource üzerinden
                WelcomeText = string.Format(AppResources.Dashboard_Welcome_WithName, userName);
            }
        }
        [RelayCommand]
        private async Task GoToProfileAsync()
        {
            await Shell.Current.GoToAsync("///ProfilePage");
        }
    }
}
