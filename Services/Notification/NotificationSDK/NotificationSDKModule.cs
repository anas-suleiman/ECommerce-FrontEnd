using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace NotificationSDK
{
    public static class NotificationSDKModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSdkClient<INotificationRESTAPIClient, NotificationRESTAPIClient>(timeOutInSeconds: 30, useRetryPolicy: false);
        }
    }
}
