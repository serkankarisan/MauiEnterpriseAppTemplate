using MauiEnterpriseApp.ViewModels.Main;

namespace MauiEnterpriseApp.Views.Main
{
    public partial class ProfilePage : ContentPage
    {
        private ProfileViewModel _viewModel;

        public ProfilePage(ProfileViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception)
            {
                // Gerekirse burada global bir hata yönetimi/loglama yapılabilir.
            }
        }
    }
}
