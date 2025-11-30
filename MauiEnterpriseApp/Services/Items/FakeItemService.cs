using MauiEnterpriseApp.Models.Items;

namespace MauiEnterpriseApp.Services.Items
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    namespace MauiEnterpriseApp.Services.Items
    {
        /// <summary>
        /// Geçici fake servis.
        /// Demo amaçlı sabit birkaç kayıt döner.
        /// </summary>
        public class FakeItemService : IItemService
        {
            public Task<IReadOnlyList<ItemSummary>> GetItemsAsync(CancellationToken cancellationToken = default)
            {
                var now = DateTime.UtcNow;

                var items = new List<ItemSummary>
            {
                new ItemSummary
                {
                    Id = "1",
                    Title = "Onay bekleyen sipariş",
                    Description = "Müşteri A için oluşturulan sipariş.",
                    Status = "Pending",
                    LastUpdatedAt = now.AddMinutes(-15),
                    ThumbnailImageData = null // İstersek buraya da image data verebiliriz
                },
                new ItemSummary
                {
                    Id = "2",
                    Title = "Aktif sözleşme",
                    Description = "2024 yılı bakım sözleşmesi.",
                    Status = "Active",
                    LastUpdatedAt = now.AddHours(-5),
                    ThumbnailImageData = null
                },
                new ItemSummary
                {
                    Id = "3",
                    Title = "Pasif kayıt",
                    Description = "Artık kullanılmayan eski kayıt.",
                    Status = "Passive",
                    LastUpdatedAt = now.AddDays(-2),
                    ThumbnailImageData = null
                }
            };

                return Task.FromResult<IReadOnlyList<ItemSummary>>(items);
            }
        }
    }
}
