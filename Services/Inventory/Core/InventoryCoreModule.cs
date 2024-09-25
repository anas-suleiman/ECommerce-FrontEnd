using InventoryCore.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryCore
{
    public static class InventoryCoreModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IInventoryManager, InventoryManager>();
        }
    }
}
