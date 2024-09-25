using InventorySDK;
using NotificationSDK;
using PaymentSDK;
using Shared;
using Shared.Models;
using System.Diagnostics;

namespace OrderManagementCore.Managers
{
    public class RESTAPIOrderManager: IRESTAPIOrderManager
    {
        private readonly IInventoryRESTAPIClient _inventoryRESTAPIClient;
        private readonly INotificationRESTAPIClient _notificationRESTAPIClient;
        private readonly IPaymentSDKClient _paymentSDKClient;

        public RESTAPIOrderManager(IInventoryRESTAPIClient inventoryRESTAPIClient, INotificationRESTAPIClient notificationRESTAPIClient, IPaymentSDKClient paymentSDKClient)
        {
            _inventoryRESTAPIClient = inventoryRESTAPIClient;
            _notificationRESTAPIClient = notificationRESTAPIClient;
            _paymentSDKClient = paymentSDKClient;
        }
        public async Task<List<ServiceResponse>> PlaceOrder(OrderItem item)
        {
            if (item == null || item.ProductId <= 0 || item.Quantity <= 0)
            {
                return default;
            }
            var paymentStopwatch = Stopwatch.StartNew();
            var paymentProcessingTime = await _paymentSDKClient.ProcessPayment(new PaymentItem { Amount = item.Payment.Amount, Method = item.Payment.Method});
            paymentStopwatch.Stop();

            var inventoryStopwatch = Stopwatch.StartNew();
            var inventoryProcessingTime = await _inventoryRESTAPIClient.UpdateInventory(new UpdateInventoryRequestModel { ProductId = item.ProductId, Quantity = item.Quantity });
            inventoryStopwatch.Stop();

            var notificationStopwatch = Stopwatch.StartNew();
            var notificationProcessingTime = await _notificationRESTAPIClient.SendNotification(new NotificationItem { EmailAddress = item.EmailAddress, Message = "Order has been placed!" });
            notificationStopwatch.Stop();


            var response = new List<ServiceResponse>();
            response.Add(new ServiceResponse { ServiceName = "PaymentService", ProcessingTime = paymentProcessingTime , Latency = paymentStopwatch.Elapsed - paymentProcessingTime });
            response.Add(new ServiceResponse { ServiceName = "InventoryService", ProcessingTime = inventoryProcessingTime, Latency = inventoryStopwatch.Elapsed - inventoryProcessingTime });
            response.Add(new ServiceResponse { ServiceName = "NotificationService", ProcessingTime = notificationProcessingTime, Latency = notificationStopwatch.Elapsed - notificationProcessingTime });

            return response;
        }
    }
}
