namespace MauiEnterpriseApp.Models.Items
{
    /// <summary>
    /// Liste ekranında gösterilecek kayıt özeti.
    /// Detay sayfası için Id taşır.
    /// </summary>
    public class ItemSummary
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        /// <summary>
        /// Örn: Aktif / Pasif / Beklemede gibi durum bilgisi.
        /// </summary>
        public string? Status { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        /// <summary>
        /// Kayıt için küçük görsel (isteğe bağlı).
        /// Url / Base64 / byte[] olabilir.
        /// </summary>
        public object? ThumbnailImageData { get; set; }
    }
}
