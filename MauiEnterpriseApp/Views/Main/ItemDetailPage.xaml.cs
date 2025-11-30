using MauiEnterpriseApp.ViewModels.Main;

namespace MauiEnterpriseApp.Views.Main
{
    public partial class ItemDetailPage : ContentPage
    {
        private readonly ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // QueryProperty ile ItemId gelmiş olacak, burada yüklemeyi tetikliyoruz
            if (_viewModel.LoadCommand.CanExecute(null))
            {
                await _viewModel.LoadCommand.ExecuteAsync(null);
            }
        }
    }
}
