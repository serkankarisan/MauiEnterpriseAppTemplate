using MauiEnterpriseApp.Models.Items;

namespace MauiEnterpriseApp.Services.Items
{
    public interface IItemService
    {
        /// <summary>
        /// Liste ekranında gösterilecek kayıt özetlerini döner.
        /// </summary>
        Task<IReadOnlyList<ItemSummary>> GetItemsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Verilen Id'ye göre kayıt detayını döner.
        /// Gerçek senaryoda API'ye gidecek.
        /// </summary>
        Task<ItemDetail?> GetItemByIdAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kayıt oluşturma / güncelleme işlemi.
        /// Id boş ise yeni kayıt oluşturur, dolu ise güncelleme yapar.
        /// Gerçek senaryoda API çağrısına bağlanır.
        /// </summary>
        Task<bool> SaveItemAsync(ItemDetail item, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verilen Id'ye göre kaydı siler.
        /// </summary>
        Task<bool> DeleteItemAsync(string id, CancellationToken cancellationToken = default);
    }
}
