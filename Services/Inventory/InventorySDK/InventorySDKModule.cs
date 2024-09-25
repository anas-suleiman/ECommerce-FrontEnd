using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace InventorySDK
{
    public static class InventorySDKModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSdkClient<IInventoryRESTAPIClient, InventoryRESTAPIClient>(timeOutInSeconds: 30, useRetryPolicy: false);            
        }
    }
}
