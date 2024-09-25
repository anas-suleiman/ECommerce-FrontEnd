using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace PaymentSDK
{
    public class PaymentSDKModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSdkClient<IPaymentSDKClient, PaymentSDKClient>(timeOutInSeconds: 30, useRetryPolicy: false);
        }
    }
}
