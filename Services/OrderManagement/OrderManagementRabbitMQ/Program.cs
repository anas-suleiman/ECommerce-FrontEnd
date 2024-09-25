using Newtonsoft.Json;
using OrderManagementRabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using Shared.Models;
using System.Text;

class OrderManagementConsumer
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare the queue to ensure it exists
            channel.QueueDeclare(queue: "order_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Set up a consumer to listen to the queue
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the message back to an OrderItem object
                OrderItem order = JsonConvert.DeserializeObject<OrderItem>(message);
                Console.WriteLine($" [x] Received Order for ProductId: {order.ProductId}, Quantity: {order.Quantity}, Price: {order.Price}, Email: {order.EmailAddress}");

                PaymentProducer.PublishPayment(new PaymentItem { Amount = order.Payment.Amount, Method = order.Payment.Method});
                InventoryProducer.PublishInventoryUpdate(new UpdateInventoryRequestModel { ProductId = order.ProductId, Quantity = order.Quantity});
                NotificationProducer.PublishNotification(new NotificationItem { EmailAddress = order.EmailAddress, Message = "Order has been placed!"});

                // Acknowledge the message
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            // Start consuming messages
            channel.BasicConsume(queue: "order_queue",
                                 autoAck: false, // Manual acknowledgment
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}
