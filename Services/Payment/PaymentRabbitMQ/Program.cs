using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Models;
using System.Text;

class PaymentConsumer
{
    public static void Main()
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

            // Set up the consumer to listen to the payment queue
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the message to PaymentItem object
                PaymentItem payment = JsonConvert.DeserializeObject<PaymentItem>(message);

                ProcessMessage(payment);

                // Acknowledge the message
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            // Start consuming messages
            channel.BasicConsume(queue: "payment_queue",
                                 autoAck: false, // Manual acknowledgment
                                 consumer: consumer);
            Console.ReadLine();
        }
    }

    static void ProcessMessage(PaymentItem payment)
    {
        // Simulate processing of the inventory update
        Thread.Sleep(100);
        Console.WriteLine($" [x] Received Payment: Amount: {payment.Amount}, Method: {payment.Method}");
    }
}
