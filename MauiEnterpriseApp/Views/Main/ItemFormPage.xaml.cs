using MauiEnterpriseApp.ViewModels.Main;

namespace MauiEnterpriseApp.Views.Main
{
    public partial class ItemFormPage : ContentPage
    {
        private readonly ItemFormViewModel _viewModel;

        public ItemFormPage(ItemFormViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.LoadCommand.CanExecute(null))
            {
                await _viewModel.LoadCommand.ExecuteAsync(null);
            }
        }
    }
}
