using MauiEnterpriseApp.Models.Items;
using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Items;

namespace MauiEnterpriseApp.ViewModels.Main
{
    [QueryProperty(nameof(ItemId), "ItemId")]
    public partial class ItemFormViewModel : BaseViewModel
    {
        private readonly IItemService _itemService;

        private string? _itemId;
        public string? ItemId
        {
            get => _itemId;
            set
            {
                if (SetProperty(ref _itemId, value))
                {
                    OnPropertyChanged(nameof(IsEditMode));
                    OnPropertyChanged(nameof(HeaderTitle));
                }
            }
        }

        public bool IsEditMode => !string.IsNullOrWhiteSpace(ItemId);

        private string _titleInput = string.Empty;
        public string TitleInput
        {
            get => _titleInput;
            set => SetProperty(ref _titleInput, value);
        }

        private string? _descriptionInput;
        public string? DescriptionInput
        {
            get => _descriptionInput;
            set => SetProperty(ref _descriptionInput, value);
        }

        private string _statusInput = string.Empty;
        public string StatusInput
        {
            get => _statusInput;
            set => SetProperty(ref _statusInput, value);
        }

        private string? _ownerInput;
        public string? OwnerInput
        {
            get => _ownerInput;
            set => SetProperty(ref _ownerInput, value);
        }

        private string? _tagsInput;
        public string? TagsInput
        {
            get => _tagsInput;
            set => SetProperty(ref _tagsInput, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public string HeaderTitle =>
            IsEditMode
                ? AppResources.ItemForm_Header_Edit
                : AppResources.ItemForm_Header_New;

        public ItemFormViewModel(IItemService itemService)
        {
            _itemService = itemService;
            Title = AppResources.ItemForm_Title;
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            if (!IsEditMode)
            {
                TitleInput = string.Empty;
                DescriptionInput = string.Empty;
                StatusInput = string.Empty;
                OwnerInput = string.Empty;
                TagsInput = string.Empty;
                StatusMessage = string.Empty;
                ErrorMessage = string.Empty;
                return;
            }

            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                StatusMessage = string.Empty;

                var detail = await _itemService.GetItemByIdAsync(ItemId!);
                if (detail == null)
                {
                    ErrorMessage = AppResources.ItemDetail_Error_NotFound;
                    return;
                }

                TitleInput = detail.Title;
                DescriptionInput = detail.Description;
                StatusInput = detail.Status ?? string.Empty;
                OwnerInput = detail.Owner;
                TagsInput = detail.Tags;
            }
            catch
            {
                ErrorMessage = AppResources.ItemDetail_Error_Generic;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            if (IsBusy)
                return;

            ErrorMessage = string.Empty;
            StatusMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(TitleInput))
            {
                ErrorMessage = AppResources.ItemForm_Validation_Title_Required;
                return;
            }

            if (string.IsNullOrWhiteSpace(StatusInput))
            {
                ErrorMessage = AppResources.ItemForm_Validation_Status_Required;
                return;
            }

            try
            {
                IsBusy = true;

                var item = new ItemDetail
                {
                    Id = ItemId ?? string.Empty,
                    Title = TitleInput.Trim(),
                    Description = string.IsNullOrWhiteSpace(DescriptionInput) ? null : DescriptionInput.Trim(),
                    Status = StatusInput.Trim(),
                    Owner = string.IsNullOrWhiteSpace(OwnerInput) ? null : OwnerInput.Trim(),
                    Tags = string.IsNullOrWhiteSpace(TagsInput) ? null : TagsInput.Trim()
                };

                var success = await _itemService.SaveItemAsync(item);
                if (!success)
                {
                    ErrorMessage = AppResources.ItemForm_Save_Error;
                    return;
                }

                // İsteğe bağlı: kısa bir mesaj set edebiliriz (debug için)
                // StatusMessage = AppResources.ItemForm_Save_Success;

                // 🔥 Kaydet başarılı → geri dön
                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                ErrorMessage = AppResources.ItemForm_Save_Error;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task NavigationBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
