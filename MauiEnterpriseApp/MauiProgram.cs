using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Api;
using MauiEnterpriseApp.Services.Auth;
using MauiEnterpriseApp.Services.Session;
using MauiEnterpriseApp.ViewModels.Auth;
using MauiEnterpriseApp.ViewModels.Main;
using MauiEnterpriseApp.Views.Auth;
using MauiEnterpriseApp.Views.Main;
using Microsoft.Extensions.Logging;
using System.Globalization;


namespace MauiEnterpriseApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // 1) Default kültürü TR yap
            var culture = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            // AppResources için de aynı culture'ı set edelim (x:Static kullanmasak da net olsun)
            AppResources.Culture = culture;

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // === Dependency Injection kayıtları ===
            // === API alt yapısı ===

            // HttpClient + ApiClient
            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                // TODO: kendi API adresini yaz
                client.BaseAddress = new Uri("https://api.ornek.com/");
                // client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Servis
            // Auth service: 
            builder.Services.AddSingleton<IAuthService, AuthApiService>();
            builder.Services.AddSingleton<ISessionService, SessionService>();

            // ViewModel
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();

            // View
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DashboardPage>();

            // ======================================

            return builder.Build();
        }
    }
}
