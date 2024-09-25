using Newtonsoft.Json;
using RabbitMQ.Client;
using Shared.Models;
using System.Text;

namespace OrderManagementRabbitMQ
{
    public class PaymentProducer
    {
        public static void PublishPayment(PaymentItem payment)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the queue for payment
                channel.QueueDeclare(queue: "payment_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Serialize the object to JSON
                string message = JsonConvert.SerializeObject(payment);
                var body = Encoding.UTF8.GetBytes(message);

                // Set the message to be persistent
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                // Send the message to the payment queue
                channel.BasicPublish(exchange: "",
                                     routingKey: "payment_queue",
                                     basicProperties: properties,
                                     body: body);

            }
        }
    }
}
