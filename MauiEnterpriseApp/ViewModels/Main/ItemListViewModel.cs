using MauiEnterpriseApp.Models.Items;
using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Items;
using MauiEnterpriseApp.Views.Main;
using System.Collections.ObjectModel;

namespace MauiEnterpriseApp.ViewModels.Main
{
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
            catch
            {
                StatusMessage = AppResources.ItemList_Error_Message;
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

            var route = $"{nameof(ItemDetailPage)}?ItemId={Uri.EscapeDataString(item.Id)}";
            await Shell.Current.GoToAsync(route);
        }

        /// <summary>
        /// Satıra uzun basılınca açılan context menü.
        /// Detay / Düzenle / Sil seçeneklerini sunar.
        /// </summary>
        [RelayCommand]
        private async Task ShowContextMenuAsync(ItemSummary? item)
        {
            if (item == null)
                return;

            var page = Shell.Current?.CurrentPage;
            if (page is null)
                return;

            var cancel = AppResources.Common_Cancel;
            var view = AppResources.ItemList_Context_View;
            var edit = AppResources.ItemList_Context_Edit;
            var delete = AppResources.Common_Delete;

            var action = await page.DisplayActionSheetAsync(
                AppResources.ItemList_Context_Title,
                cancel,
                null,
                view,
                edit,
                delete);

            if (string.IsNullOrEmpty(action) || action == cancel)
                return;

            if (action == view)
            {
                await ItemTappedAsync(item);
            }
            else if (action == edit)
            {
                var route = $"{nameof(ItemFormPage)}?ItemId={Uri.EscapeDataString(item.Id)}";
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync(route);
                }
            }
            else if (action == delete)
            {
                await DeleteItemInternalAsync(item);
            }
        }

        [RelayCommand]
        private async Task NewItemAsync()
        {
            await Shell.Current.GoToAsync(nameof(ItemFormPage));
        }

        /// <summary>
        /// Kayıt silme işlemini yapar ve listeyi günceller.
        /// </summary>
        private async Task DeleteItemInternalAsync(ItemSummary item)
        {
            var page = Shell.Current?.CurrentPage;
            if (page is null)
                return;

            bool confirm = await page.DisplayAlertAsync(
                AppResources.ItemList_Delete_Confirm_Title,
                string.Format(AppResources.ItemList_Delete_Confirm_Message, item.Title),
                AppResources.Common_Yes,
                AppResources.Common_No);

            if (!confirm)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                StatusMessage = string.Empty;

                var success = await _itemService.DeleteItemAsync(item.Id);
                if (!success)
                {
                    ErrorMessage = AppResources.ItemList_Delete_Error;
                    return;
                }

                if (Items.Contains(item))
                {
                    Items.Remove(item);
                }

                StatusMessage = AppResources.ItemList_Delete_Success;
            }
            catch
            {
                ErrorMessage = AppResources.ItemList_Delete_Error;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}