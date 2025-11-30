using MauiEnterpriseApp.ViewModels.Main;

namespace MauiEnterpriseApp.Views.Main
{
    public partial class ItemListPage : ContentPage
    {
        private readonly ItemListViewModel _viewModel;

        public ItemListPage(ItemListViewModel viewModel)
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
