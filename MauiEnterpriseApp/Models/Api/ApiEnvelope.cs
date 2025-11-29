namespace MauiEnterpriseApp.Models.Api
{
    /// <summary>
    /// Backend tarafında yaygın kullanılan "IDataResult" benzeri zarf.
    /// Success + Message + Data şeklinde iş sonucunu temsil eder.
    /// </summary>
    public class ApiEnvelope<T>
    {
        /// <summary>
        /// İşlemsel anlamda başarı durumu (login başarılı mı, kayıt oldu mu vs).
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Frontend'de lokalize edilebilecek hata/mesaj kodu.
        /// Örn: "LOGIN_INVALID_CREDENTIALS", "USER_LOCKED".
        /// </summary>
        public string? MessageCode { get; set; }

        /// <summary>
        /// Backend'in döndürdüğü raw mesaj (log için, UI'ya direkt verilmez).
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Asıl veri payload'ı.
        /// Örn: LoginResponse gibi.
        /// </summary>
        public T? Data { get; set; }
    }
}
