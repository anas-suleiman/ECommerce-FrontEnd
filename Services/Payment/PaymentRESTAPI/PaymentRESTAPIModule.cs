using PaymentCore;

namespace PaymentRESTAPI
{
    public static class PaymentRESTAPIModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            PaymentCoreModule.RegisterServices(services);
        }
    }
}
