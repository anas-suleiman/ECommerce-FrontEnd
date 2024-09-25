using Shared.Models;

namespace PaymentCore.Managers
{
    public interface IPaymentManager
    {
        Task<TimeSpan> ProcessPayment(PaymentItem item);
    }
}
