using Microsoft.Extensions.DependencyInjection;
using NotificationCore.Managers;

namespace NotificationCore
{
    public static class NotificationCoreModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<INotificationManager, NotificationManager>();
        }
    }
}
