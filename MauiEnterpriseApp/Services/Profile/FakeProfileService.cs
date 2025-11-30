using MauiEnterpriseApp.Models.Profile;
using MauiEnterpriseApp.Services.Session;

namespace MauiEnterpriseApp.Services.Profile
{
    /// <summary>
    /// Geçici (fake) profil servisi.
    /// Login sonrası SessionService'ten kullanıcıyı alır, demo profil döner.
    /// Gerçek API entegrasyonu için bu sınıf yerine ProfileApiService yazılabilir.
    /// </summary>
    public class FakeProfileService : IProfileService
    {
        private readonly ISessionService _sessionService;

        public FakeProfileService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public Task<UserProfile?> GetCurrentProfileAsync(CancellationToken cancellationToken = default)
        {
            var currentUser = _sessionService.CurrentUser;
            if (currentUser == null)
            {
                return Task.FromResult<UserProfile?>(null);
            }

            var profile = new UserProfile
            {
                FullName = string.IsNullOrWhiteSpace(currentUser.UserName)
                    ? "Demo Kullanıcı"
                    : currentUser.UserName!,
                Email = null, // LoginResponse'a Email eklersen burayı doldurabilirsin
                Department = "Yazılım Geliştirme",
                Position = "Yazılım Mühendisi",
                PhoneNumber = "+90 555 000 00 00",
                JoinedAt = DateTime.UtcNow.AddYears(-1),

                // ÖRNEK 1: URL ile görsel (şu an aktif senaryo)
                // Bu tarz placeholder servisler demo için idealdir.
                ProfileImageData = "https://avatars.githubusercontent.com/u/9919?s=200&v=4",

                // ÖRNEK 2: Base64 ile denemek istersen:
                // ProfileImageData = "iVBORw0KGgoAAAANSUhEUgAA...",

                // ÖRNEK 3: byte[] kullanmak istersen (örnek):
                // ProfileImageData = someByteArray
            };

            return Task.FromResult<UserProfile?>(profile);
        }
    }
}
