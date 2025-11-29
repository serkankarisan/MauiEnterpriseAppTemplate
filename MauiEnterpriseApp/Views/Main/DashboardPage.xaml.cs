using MauiEnterpriseApp.ViewModels.Main;

namespace MauiEnterpriseApp.Views.Main
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage(DashboardViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
