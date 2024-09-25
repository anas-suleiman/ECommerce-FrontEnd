using Microsoft.AspNetCore.Mvc;
using NotificationCore.Managers;
using Shared;

namespace InventoryRESTAPI.Controllers
{
    [ApiController]

    public class NotificationController : ControllerBase
    {
        private readonly INotificationManager _notificationManager;

        public NotificationController(INotificationManager inventoryManager)
        {
            _notificationManager = inventoryManager;
        }

        [HttpGet("/")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpPost("SendNotification")]
        public async Task<TimeSpan> SendNotification([FromBody] NotificationItem request)
        {
            return await _notificationManager.SendNotification(request);
        }
    }
}
