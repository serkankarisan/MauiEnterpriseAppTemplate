namespace MauiEnterpriseApp.Models.Items
{
    /// <summary>
    /// Kayıt detayı için model.
    /// Liste ekranındaki ItemSummary'den daha zengin alanlar içerir.
    /// </summary>
    public class ItemDetail
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public string? Owner { get; set; }

        /// <summary>
        /// Etiketler, virgülle ayrılmış text veya ileride ayrı tabloya bağlanabilir.
        /// </summary>
        public string? Tags { get; set; }

        /// <summary>
        /// Detay ekranında kullanılabilecek görsel verisi (Url / Base64 / byte[]).
        /// </summary>
        public object? ImageData { get; set; }
    }
}
