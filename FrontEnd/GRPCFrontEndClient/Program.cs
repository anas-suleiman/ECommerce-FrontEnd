using Grpc.Net.Client;
using GRPCFrontEndClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7224");
var client = new OrderManagement.OrderManagementClient(channel);
var reply = await client.PlaceOrderAsync(
                  new OrderItem { ProductId = "1", Quantity = "1", Price = "5", Emailaddress = "user@uni.com",  Amount = "100", Method = "CreditCard" });
Console.WriteLine($"ProcessingTime: {reply.ProcessingTime}\nLatency:{reply.Latency}");
Console.WriteLine();

Console.WriteLine("Processing 500 requests");

//var taskList = new List<Task<AsyncUnaryCall<ServiceResponse>>>();
var taskList = new List<Task<ServiceResponse>>();

for (int i = 0; i < 500; i++)
{
    taskList.Add(Task.Run(async () => await client.PlaceOrderAsync(new OrderItem { ProductId = "1", Quantity = "1", Price = "5", Emailaddress = "user@uni.com", Amount = "100", Method = "CreditCard" })));
}

var resultLists = await Task.WhenAll(taskList);
var processingTime = resultLists.Sum(s => Int32.Parse(s.ProcessingTime));
var latency = resultLists.Sum(s => Int32.Parse(s.Latency));
Console.WriteLine($"ProcessingTime: {processingTime}\nLatency:{latency}");

Console.ReadKey();