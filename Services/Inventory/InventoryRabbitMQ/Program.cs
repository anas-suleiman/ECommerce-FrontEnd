using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Models;
using System.Text;

class InventoryConsumer
{
    public static void Main()
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

            // Set up the consumer to listen to the inventory update queue
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the message to UpdateInventoryRequestModel object
                UpdateInventoryRequestModel updateRequest = JsonConvert.DeserializeObject<UpdateInventoryRequestModel>(message);

                ProcessMessage(updateRequest);

                // Acknowledge the message
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            // Start consuming messages
            channel.BasicConsume(queue: "inventory_queue",
                                 autoAck: false, // Manual acknowledgment
                                 consumer: consumer);

            Console.ReadLine();
        }
    }

    static void ProcessMessage(UpdateInventoryRequestModel updateRequest)
    {
        // Simulate processing of the inventory update
        Thread.Sleep(100);
        Console.WriteLine($" [x] Received Inventory Update: ProductId: {updateRequest.ProductId}, Quantity: {updateRequest.Quantity}");
    }
}
