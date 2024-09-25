using InventoryCore;

namespace InventoryRESTAPI
{
    public static class InventoryRESTAPIModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            InventoryCoreModule.RegisterServices(services);
        }
    }
}
