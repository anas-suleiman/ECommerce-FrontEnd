using Newtonsoft.Json;
using RabbitMQ.Client;
using Shared.Models;
using System.Text;

namespace OrderManagementRabbitMQ
{
    public class InventoryProducer
    {
        public static void PublishInventoryUpdate(UpdateInventoryRequestModel updateRequest)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the inventory update queue
                channel.QueueDeclare(queue: "inventory_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Serialize the object to JSON
                string message = JsonConvert.SerializeObject(updateRequest);
                var body = Encoding.UTF8.GetBytes(message);

                // Set the message to be persistent
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                // Send the message to the inventory update queue
                channel.BasicPublish(exchange: "",
                                     routingKey: "inventory_queue",
                                     basicProperties: properties,
                                     body: body);

            }


        }

    }
}
