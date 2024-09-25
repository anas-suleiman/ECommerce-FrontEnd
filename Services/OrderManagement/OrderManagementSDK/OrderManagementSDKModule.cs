using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace OrderManagementSDK
{
    public static class OrderManagementSDKModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSdkClient<IOrderManagementRESTAPIClient, OrderManagementRESTAPIClient>(timeOutInSeconds: 30, useRetryPolicy: false);
        }
    }
}
