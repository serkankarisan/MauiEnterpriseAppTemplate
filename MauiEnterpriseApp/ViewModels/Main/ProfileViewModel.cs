using MauiEnterpriseApp.Helpers.Images;
using MauiEnterpriseApp.Models.Profile;
using MauiEnterpriseApp.Resources.Localization;
using MauiEnterpriseApp.Services.Profile;

namespace MauiEnterpriseApp.ViewModels.Main
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;

        private UserProfile? _profile;
        public UserProfile? Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private ImageSource? _profileImage;
        public ImageSource? ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        private const string DefaultProfileImageName = "no_image.png";

        public ProfileViewModel(IProfileService profileService)
        {
            _profileService = profileService;
            Title = AppResources.Profile_Title;
        }

        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                StatusMessage = AppResources.Profile_Loading_Message;
                ErrorMessage = string.Empty;

                var result = await _profileService.GetCurrentProfileAsync();

                if (result == null)
                {
                    Profile = null;
                    ProfileImage = ImageSourceHelper.GetImageSourceFromData(null, DefaultProfileImageName);
                    StatusMessage = AppResources.Profile_Empty_Message;
                    return;
                }

                Profile = result;
                ProfileImage = ImageSourceHelper.GetImageSourceFromData(result.ProfileImageData, DefaultProfileImageName);
                StatusMessage = string.Empty;
            }
            catch (Exception)
            {
                Profile = null;
                ProfileImage = ImageSourceHelper.GetImageSourceFromData(null, DefaultProfileImageName);
                StatusMessage = AppResources.Profile_Empty_Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
