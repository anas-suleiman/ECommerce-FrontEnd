using InventorySDK;
using Microsoft.Extensions.DependencyInjection;
using NotificationSDK;
using OrderManagementCore.Managers;
using PaymentSDK;

namespace OrderManagementCore
{
    public static class OrderManagementCoreModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRESTAPIOrderManager, RESTAPIOrderManager>();
            InventorySDKModule.RegisterServices(services);
            PaymentSDKModule.RegisterServices(services);
            NotificationSDKModule.RegisterServices(services);
        }
    }
}
