using Shared.Models;

namespace InventoryCore.Managers
{
    public interface IInventoryManager
    {
        Task<TimeSpan> UpdateInventory(UpdateInventoryRequestModel request);
    }
}