using Microsoft.Extensions.DependencyInjection;
using PaymentCore.Managers;

namespace PaymentCore
{
    public static class PaymentCoreModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPaymentManager, PaymentManager>();
        }
    }
}
