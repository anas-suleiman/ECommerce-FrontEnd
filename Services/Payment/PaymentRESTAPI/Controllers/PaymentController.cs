using Microsoft.AspNetCore.Mvc;
using PaymentCore.Managers;
using Shared.Models;

namespace PaymentRESTAPI.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;


        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        [HttpGet("/")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpPost("ProcessPayment")]
        public async Task<TimeSpan> ProcessPayment([FromBody] PaymentItem request)
        {
            return await _paymentManager.ProcessPayment(request);
        }
    }
}
