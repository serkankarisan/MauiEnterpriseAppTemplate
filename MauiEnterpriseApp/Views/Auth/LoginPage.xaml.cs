using MauiEnterpriseApp.ViewModels.Auth;

namespace MauiEnterpriseApp.Views.Auth
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
