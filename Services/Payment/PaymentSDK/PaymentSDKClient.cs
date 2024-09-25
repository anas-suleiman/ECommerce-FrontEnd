using Shared;
using Shared.Models;

namespace PaymentSDK
{
    public class PaymentSDKClient : BaseSdkClient, IPaymentSDKClient
    {
        public PaymentSDKClient(HttpClient httpClient) : base("https://localhost:7177", httpClient) { }

        public async Task<TimeSpan> ProcessPayment(PaymentItem request)
        {
            return await SendJsonRequest<TimeSpan>(HttpMethod.Post, "ProcessPayment", request);
        }
    }
}
