using OrderManagementCore;

namespace OrderManagementRESTAPI
{
    public class OrderManagementRESTAPIModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            OrderManagementCoreModule.RegisterServices(services);
        }
    }
}
