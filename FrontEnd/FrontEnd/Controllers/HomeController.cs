using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSDK;
using Shared.Models;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderManagementRESTAPIClient _orderManagementRESTAPIClient;
        public HomeController(ILogger<HomeController> logger, IOrderManagementRESTAPIClient orderManagementRESTAPIClient)
        {
            _logger = logger;
            _orderManagementRESTAPIClient = orderManagementRESTAPIClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _orderManagementRESTAPIClient.PlaceOrder(new OrderItem { ProductId = 1, Quantity = 1, Price = 5, EmailAddress = "user@uni.com", Payment = new PaymentItem { Amount = 100, Method = "CreditCard"} });
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/RESTAPISingleOrder")]
        public async Task<IActionResult> RESTAPISingleOrder()
        {
         _logger.LogInformation("RESTAPISingleOrder");

        var response = await _orderManagementRESTAPIClient.PlaceOrder(new OrderItem { ProductId = 1, Quantity = 1, Price = 5, EmailAddress = "user@uni.com", Payment = new PaymentItem { Amount = 100, Method = "CreditCard" } });
        return Ok(FormatMessage(response));

        }

        [HttpGet("/RESTAPIXOrders/{ordersCount}")]
        public async Task<IActionResult> RESTAPIXOrders(int ordersCount)
        {
            _logger.LogInformation($"RESTAPIXOrders(ordersCount : {ordersCount})");

            var taskList = new List<Task<List<ServiceResponse>>>();
            for (int i = 0; i < ordersCount; i++)
            {
                taskList.Add(_orderManagementRESTAPIClient.PlaceOrder(new OrderItem { ProductId = 1, Quantity = 1, Price = 5, EmailAddress = "user@uni.com", Payment = new PaymentItem { Amount = 100, Method = "CreditCard" } }));
            }

            List<ServiceResponse>[] resultLists = await Task.WhenAll(taskList);
            return Ok(FormatMessages(resultLists.ToList()));
        }


        private string FormatMessage(List<ServiceResponse> response)
        {
            _logger.LogDebug($"FormatMessage()");

            var processingTime = response.Sum(s => s.ProcessingTime.Milliseconds);
            var latency = response.Sum(s => s.Latency.Milliseconds);
            var message = $"ProcessingTime: {processingTime}\nLatency: {latency}";
            return message;
        }

        private string FormatMessages(List<List<ServiceResponse>> responses)
        {
            _logger.LogDebug($"FormatMessages()");

            long processingTime = 0;
            long latency = 0;
            foreach (var response in responses)
            {
                processingTime += response.Sum(s => s.ProcessingTime.Milliseconds);
                latency += response.Sum(s => s.Latency.Milliseconds);
            }          
            var message = $"ProcessingTime: {processingTime}\nLatency: {latency}";
            return message;
        }
    }
}
