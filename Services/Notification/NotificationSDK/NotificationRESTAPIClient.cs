using Shared;

namespace NotificationSDK
{
    internal class NotificationRESTAPIClient : BaseSdkClient, INotificationRESTAPIClient
    {
        public NotificationRESTAPIClient(HttpClient httpClient) : base("https://localhost:7215", httpClient) { }


        public async Task<TimeSpan> SendNotification(NotificationItem request)
        {
            return await SendJsonRequest<TimeSpan>(HttpMethod.Post, "SendNotification", request);
        }
    }
}
