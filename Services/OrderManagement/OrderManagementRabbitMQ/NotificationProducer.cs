using Newtonsoft.Json;
using RabbitMQ.Client;
using Shared;
using System.Text;

namespace OrderManagementRabbitMQ
{
    public class NotificationProducer
    {
        public static void PublishNotification(NotificationItem notificationItem)
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


                // Serialize the object to JSON
                string message = JsonConvert.SerializeObject(notificationItem);
                var body = Encoding.UTF8.GetBytes(message);

                // Set the message to be persistent
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                // Send the message to the notification queue
                channel.BasicPublish(exchange: "",
                                     routingKey: "notification_queue",
                                     basicProperties: properties,
                                     body: body);

            }
        }

    }
}
