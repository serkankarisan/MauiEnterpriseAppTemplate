using MauiEnterpriseApp.Models.Profile;

namespace MauiEnterpriseApp.Services.Profile
{
    public interface IProfileService
    {
        /// <summary>
        /// Oturumdaki kullanıcıya ait profil bilgilerini getirir.
        /// </summary>
        Task<UserProfile?> GetCurrentProfileAsync(CancellationToken cancellationToken = default);
    }
}
