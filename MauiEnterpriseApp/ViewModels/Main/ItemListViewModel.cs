using MauiEnterpriseApp.Models.Items;
using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Items;
using MauiEnterpriseApp.Views.Main;
using System.Collections.ObjectModel;

namespace MauiEnterpriseApp.ViewModels.Main
{
    /// <summary>
    /// Liste / Detay ana liste ekranı için ViewModel.
    /// </summary>
    public partial class ItemListViewModel : BaseViewModel
    {
        private readonly IItemService _itemService;

        public ObservableCollection<ItemSummary> Items { get; } = new();

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ItemListViewModel(IItemService itemService)
        {
            _itemService = itemService;

            Title = AppResources.ItemList_Title;
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                StatusMessage = string.Empty;
                ErrorMessage = string.Empty;

                Items.Clear();

                var result = await _itemService.GetItemsAsync();

                if (result == null || result.Count == 0)
                {
                    StatusMessage = AppResources.ItemList_Empty_Message;
                    return;
                }

                foreach (var item in result)
                {
                    Items.Add(item);
                }
            }
            catch (Exception)
            {
                StatusMessage = AppResources.ItemList_Error_Message;
                // Teknik detayı logger ile kaydedebiliriz.
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            IsRefreshing = true;
            await LoadAsync();
        }

        [RelayCommand]
        private async Task ItemTappedAsync(ItemSummary? item)
        {
            if (item == null)
                return;

            // Shell route: AppShell.xaml.cs içinde nameof(ItemDetailPage) ile register edildi
            var route = $"{nameof(ItemDetailPage)}?ItemId={Uri.EscapeDataString(item.Id)}";

            await Shell.Current.GoToAsync(route);
        }
    }
}
