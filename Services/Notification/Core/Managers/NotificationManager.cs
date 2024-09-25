using Shared;
using System.Diagnostics;

namespace NotificationCore.Managers
{
    public class NotificationManager : INotificationManager
    {
        public async Task<TimeSpan> SendNotification(NotificationItem item)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate processing of sending notification
            await Task.Delay(100);
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }
    }
}
