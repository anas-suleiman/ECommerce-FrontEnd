using Grpc.Core;
using Grpc.Net.Client;
using InventoryRPCClient;
using NotificationRPCClient;
using PaymentRPCClient;
using System.Diagnostics;

namespace OrderManagementRPC.Services
{
    public class OrderManagementService : OrderManagement.OrderManagementBase
    {
        private readonly ILogger<OrderManagementService> _logger;
        public OrderManagementService(ILogger<OrderManagementService> logger)
        {
            _logger = logger;
        }

        public override async Task<ServiceResponse> PlaceOrder(OrderItem request, ServerCallContext context)
        {
            var paymentStopwatch = Stopwatch.StartNew();
            using var paymentChannel = GrpcChannel.ForAddress("https://localhost:7046");
            var paymentClient = new Payment.PaymentClient(paymentChannel);
            var paymentResponse = await paymentClient.ProcessPaymentAsync(new PaymentItem { Amount = "10", Method = "CreditCard" });
            paymentStopwatch.Stop();

            var inventoryStopwatch = Stopwatch.StartNew();
            using var inventoryChannel = GrpcChannel.ForAddress("https://localhost:7285");
            var inventoryClient = new Inventory.InventoryClient(inventoryChannel);
            var inventoryResponse = await inventoryClient.UpdateInventoryAsync(new UpdateInventoryRequestModel { ProductId = "1", Quantity = "1", });
            inventoryStopwatch.Stop();

            var notificationStopwatch = Stopwatch.StartNew();
            using var notificationChannel = GrpcChannel.ForAddress("https://localhost:7108");
            var notificationClient = new Notification.NotificationClient(notificationChannel);
            var notificationResponse = await notificationClient.SendNotificationAsync(new NotificationItem { Emailaddress = "user@uni.com", Message = "Order has been placed!", });
            notificationStopwatch.Stop();


            var ProcessingTime = (Int32.Parse(paymentResponse.ProcessingTime) + Int32.Parse(inventoryResponse.ProcessingTime) + Int32.Parse(notificationResponse.ProcessingTime));
            var Latency = (paymentStopwatch.ElapsedMilliseconds - Int32.Parse(paymentResponse.ProcessingTime)) + (inventoryStopwatch.ElapsedMilliseconds - Int32.Parse(inventoryResponse.ProcessingTime)) + (notificationStopwatch.ElapsedMilliseconds - Int32.Parse(notificationResponse.ProcessingTime));
            return new ServiceResponse
            {
                ProcessingTime = ProcessingTime.ToString(),
                Latency = Latency.ToString()
            };


        }
    }
}
