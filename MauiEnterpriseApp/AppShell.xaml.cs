using System.Windows.Input;

namespace MauiEnterpriseApp;

public partial class AppShell : Shell
{
    public ICommand CloseFlyoutCommand { get; }
    public ICommand ToggleThemeCommand { get; }

    public AppShell()
    {
        // Shell içinden kullanılacak komutlar
        CloseFlyoutCommand = new Command(() => FlyoutIsPresented = false);
        ToggleThemeCommand = new Command(ToggleTheme);

        InitializeComponent();
    }

    private void ToggleTheme()
    {
        var app = Application.Current;
        if (app is null)
            return;

        app.UserAppTheme = app.UserAppTheme switch
        {
            AppTheme.Light => AppTheme.Dark,
            AppTheme.Dark => AppTheme.Light,
            _ => AppTheme.Dark
        };
    }

    protected override bool OnBackButtonPressed()
    {
        // Flyout açıksa önce onu kapat (ayrı kapatma butonuna gerek yok)
        if (FlyoutIsPresented)
        {
            FlyoutIsPresented = false;
            return true; // geri event'ini tükettik
        }

        return base.OnBackButtonPressed();
    }
}
