using Shared.Models;
using System.Diagnostics;

namespace PaymentCore.Managers
{
    public class PaymentManager : IPaymentManager
    {
        public async Task<TimeSpan> ProcessPayment(PaymentItem item)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate processing of processing payment
            await Task.Delay(100);
          
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
