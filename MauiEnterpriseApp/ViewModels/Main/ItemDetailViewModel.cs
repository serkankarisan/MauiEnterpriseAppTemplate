using MauiEnterpriseApp.Models.Items;
using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Items;

namespace MauiEnterpriseApp.ViewModels.Main
{
    /// <summary>
    /// Kayıt detayı ekranı için ViewModel.
    /// </summary>
    [QueryProperty(nameof(ItemId), "ItemId")]
    public partial class ItemDetailViewModel : BaseViewModel
    {
        private readonly IItemService _itemService;

        private string? _itemId;
        public string? ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value);
        }

        private ItemDetail? _item;
        public ItemDetail? Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ItemDetailViewModel(IItemService itemService)
        {
            _itemService = itemService;
            Title = AppResources.ItemDetail_Title;
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                StatusMessage = AppResources.ItemDetail_Loading_Message;

                if (string.IsNullOrWhiteSpace(ItemId))
                {
                    Item = null;
                    StatusMessage = AppResources.ItemDetail_Error_NotFound;
                    return;
                }

                var detail = await _itemService.GetItemByIdAsync(ItemId);

                if (detail == null)
                {
                    Item = null;
                    StatusMessage = AppResources.ItemDetail_Error_NotFound;
                    return;
                }

                Item = detail;
                StatusMessage = string.Empty;
            }
            catch (Exception)
            {
                Item = null;
                StatusMessage = AppResources.ItemDetail_Error_Generic;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
