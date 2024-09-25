using Newtonsoft.Json;
using RabbitMQ.Client;
using Shared.Models;
using System.Diagnostics;
using System.Text;

class FrontEndProducer
{
    public static async Task Main(string[] args)
    {
        var stopWatch = Stopwatch.StartNew();
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare a queue
            channel.QueueDeclare(queue: "order_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var taskList = new List<Task>();
            for (int i = 0; i < 50; i++)
            {
                OrderItem order = new OrderItem
                {
                    ProductId = 1,
                    Quantity = 2,
                    Price = 10,
                    EmailAddress = "user@uni.com",
                    Payment = new PaymentItem
                    {
                        Method = "CreditCard",
                        Amount = 10
                    }
                };

                taskList.Add(CreateTask(channel, order));
            }

            await Task.WhenAll(taskList);
            stopWatch.Stop();

            Console.WriteLine($"Sending 50 requests has completed and ElapsedMilliseconds is : {stopWatch.ElapsedMilliseconds}");

        }
        Console.ReadLine();
    }

    private static Task CreateTask(IModel channel, OrderItem order)
    {
        return Task.Run(() => {
            string message = JsonConvert.SerializeObject(order);
            var body = Encoding.UTF8.GetBytes(message);

            // Send the message
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: "order_queue",
                                 basicProperties: properties,
                                 body: body);
        });
       
    }
}
