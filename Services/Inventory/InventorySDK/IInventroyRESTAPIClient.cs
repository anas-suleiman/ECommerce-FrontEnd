using Shared.Models;

namespace InventorySDK
{
    public interface IInventoryRESTAPIClient
    {
        Task<TimeSpan> UpdateInventory(UpdateInventoryRequestModel request);
    }
}
