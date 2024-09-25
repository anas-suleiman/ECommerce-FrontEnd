using Grpc.Core;
using System.Diagnostics;

namespace NotificationRPC.Services
{
    public class NotificationService : Notification.NotificationBase
    {
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public override async Task<NotificationReply> SendNotification(NotificationItem request, ServerCallContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate processing of sending notification
            await Task.Delay(100);

            stopwatch.Stop();

            return new NotificationReply
            {
                ProcessingTime = stopwatch.ElapsedMilliseconds.ToString()
            };          
        }
    }
}
