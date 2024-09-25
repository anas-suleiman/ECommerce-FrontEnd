using Shared;
using Shared.Models;

namespace NotificationCore.Managers
{
    public interface INotificationManager
    {
        Task<TimeSpan> SendNotification(NotificationItem item);
    }
}
