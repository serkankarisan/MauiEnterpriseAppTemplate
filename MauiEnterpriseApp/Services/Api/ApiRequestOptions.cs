using MauiEnterpriseApp.Services.Api.MauiEnterpriseApp.Services.Api;

namespace MauiEnterpriseApp.Services.Api
{
    /// <summary>
    /// API isteği için ek ayarlar: header'lar, auth vb.
    /// </summary>
    public class ApiRequestOptions
    {
        /// <summary>
        /// İsteğe özel header'lar.
        /// Örn: X-Integration-Key, X-Tenant-Id vb.
        /// </summary>
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        /// <summary>
        /// İsteğe özel yetkilendirme bilgisi.
        /// Eğer null veya Scheme = None ise, Authorization header set edilmez.
        /// </summary>
        public ApiAuthOptions? Auth { get; set; }
    }
}
