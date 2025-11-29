namespace MauiEnterpriseApp.Services.Api
{
    namespace MauiEnterpriseApp.Services.Api
    {
        /// <summary>
        /// Bir API isteği için yetkilendirme ayarları.
        /// Authorization header'ını burada tarif ederiz.
        /// </summary>
        public class ApiAuthOptions
        {
            /// <summary>
            /// Kullanılacak auth tipi (None, Bearer, Basic, Custom).
            /// </summary>
            public ApiAuthScheme Scheme { get; set; } = ApiAuthScheme.None;

            /// <summary>
            /// Bearer veya Custom şemalar için token veya credential değeri.
            /// Örn: JWT token, integration key vb.
            /// </summary>
            public string? Token { get; set; }

            /// <summary>
            /// Basic auth için kullanıcı adı.
            /// </summary>
            public string? UserName { get; set; }

            /// <summary>
            /// Basic auth için parola.
            /// </summary>
            public string? Password { get; set; }

            /// <summary>
            /// Custom auth kullanırken "Authorization: {CustomSchemeName} {Token}" formatı için.
            /// Örn: "ApiKey", "Integration".
            /// </summary>
            public string? CustomSchemeName { get; set; }
        }
    }
}
