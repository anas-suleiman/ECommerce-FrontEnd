using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System.Text;

class NotificationConsumer
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare the notification queue
            channel.QueueDeclare(queue: "notification_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Set up the consumer to listen to the notification queue
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the message to NotificationItem object
                NotificationItem notificationItem = JsonConvert.DeserializeObject<NotificationItem>(message);

                ProcessMessage(notificationItem);

                // Acknowledge the message
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            // Start consuming messages
            channel.BasicConsume(queue: "notification_queue",
                                 autoAck: false, // Manual acknowledgment
                                 consumer: consumer);

            Console.ReadLine();
        }
    }

    static void ProcessMessage(NotificationItem notificationItem)
    {
        // Simulate processing of the inventory update
        Thread.Sleep(100);
        Console.WriteLine($" [x] Received Notification for {notificationItem.EmailAddress}: {notificationItem.Message}");
    }
}
