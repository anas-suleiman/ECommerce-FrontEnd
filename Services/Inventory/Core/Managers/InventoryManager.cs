using Shared.Models;
using System.Diagnostics;

namespace InventoryCore.Managers
{
    public class InventoryManager: IInventoryManager
    {

        public async Task<TimeSpan> UpdateInventory(UpdateInventoryRequestModel request)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate processing of the inventory update
            await Task.Delay(100);

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }
    }
}
