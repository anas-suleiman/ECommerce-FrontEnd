using NotificationCore;

namespace NotificationRESTAPI
{
    public static class NotificationRESTAPIModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            NotificationCoreModule.RegisterServices(services);
        }
    }
}
