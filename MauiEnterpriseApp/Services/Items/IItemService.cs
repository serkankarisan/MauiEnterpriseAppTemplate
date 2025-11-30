using MauiEnterpriseApp.Models.Items;

namespace MauiEnterpriseApp.Services.Items
{
    public interface IItemService
    {
        /// <summary>
        /// Liste ekranında gösterilecek kayıtları döner.
        /// İleride gerçek API çağrısına bağlanabilir.
        /// </summary>
        Task<IReadOnlyList<ItemSummary>> GetItemsAsync(CancellationToken cancellationToken = default);
    }
}
