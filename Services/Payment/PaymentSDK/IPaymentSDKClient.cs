using Shared.Models;

namespace PaymentSDK
{
    public interface IPaymentSDKClient
    {
        Task<TimeSpan> ProcessPayment(PaymentItem request);
    }
}
