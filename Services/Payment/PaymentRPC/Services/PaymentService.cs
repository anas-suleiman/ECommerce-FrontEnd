using Grpc.Core;
using System.Diagnostics;

namespace PaymentRPC.Services
{
    public class PaymentService : Payment.PaymentBase
    {
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public override async Task<PaymentReply> ProcessPayment(PaymentItem request, ServerCallContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate processing of processing payment
            await Task.Delay(100);

            stopwatch.Stop();

            return new PaymentReply
            {
                ProcessingTime = stopwatch.ElapsedMilliseconds.ToString()
            };
        }
    }
}
