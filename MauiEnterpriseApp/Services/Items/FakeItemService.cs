using MauiEnterpriseApp.Models.Items;

namespace MauiEnterpriseApp.Services.Items
{
    /// <summary>
    /// Geçici fake servis.
    /// Demo amaçlı sabit kayıtlar döner.
    /// </summary>
    public class FakeItemService : IItemService
    {
        // Demo data kaynağı: hem liste hem detay buradan beslenecek
        private static readonly List<ItemDetail> _items = new()
        {
            new ItemDetail
            {
                Id = "1",
                Title = "Onay bekleyen sipariş",
                Description = "Müşteri A için oluşturulan sipariş. Onay sürecinde.",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow.AddHours(-8),
                LastUpdatedAt = DateTime.UtcNow.AddMinutes(-15),
                Owner = "Serkan Karışan",
                Tags = "Sipariş,Onay,Beklemede",
                ImageData = null
            },
            new ItemDetail
            {
                Id = "2",
                Title = "Aktif sözleşme",
                Description = "2024 yılı bakım ve destek sözleşmesi.",
                Status = "Active",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                LastUpdatedAt = DateTime.UtcNow.AddHours(-5),
                Owner = "Yazılım Ekibi",
                Tags = "Sözleşme,Aktif",
                ImageData = null
            },
            new ItemDetail
            {
                Id = "3",
                Title = "Pasif kayıt",
                Description = "Artık kullanılmayan eski kayıt.",
                Status = "Passive",
                CreatedAt = DateTime.UtcNow.AddYears(-1),
                LastUpdatedAt = DateTime.UtcNow.AddDays(-2),
                Owner = "Arşiv",
                Tags = "Arşiv,Pasif",
                ImageData = null
            }
        };

        public Task<IReadOnlyList<ItemSummary>> GetItemsAsync(CancellationToken cancellationToken = default)
        {
            var summaries = _items
                .Select(x => new ItemSummary
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    LastUpdatedAt = x.LastUpdatedAt,
                    ThumbnailImageData = null // İleride x.ImageData'dan türetebilirsin
                })
                .ToList()
                .AsReadOnly();

            return Task.FromResult<IReadOnlyList<ItemSummary>>(summaries);
        }

        public Task<ItemDetail?> GetItemByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult<ItemDetail?>(item);
        }
    }
}
