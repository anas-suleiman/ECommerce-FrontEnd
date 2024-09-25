using Shared;

namespace NotificationSDK
{
    public interface INotificationRESTAPIClient
    {
        Task<TimeSpan> SendNotification(NotificationItem request);
    }
}
