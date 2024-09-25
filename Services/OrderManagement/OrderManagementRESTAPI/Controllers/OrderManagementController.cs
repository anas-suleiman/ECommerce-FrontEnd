using Microsoft.AspNetCore.Mvc;
using OrderManagementCore.Managers;
using Shared.Models;

namespace OrderManagementRESTAPI.Controllers
{
    public class OrderManagementController : ControllerBase
    {
        private readonly IRESTAPIOrderManager _orderManager;

        public OrderManagementController(IRESTAPIOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet("/")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpPost("PlaceOrder")]
        public async Task<List<ServiceResponse>> PlaceOrder([FromBody] OrderItem request)
        {
            return await _orderManager.PlaceOrder(request);
        }
    }
}
