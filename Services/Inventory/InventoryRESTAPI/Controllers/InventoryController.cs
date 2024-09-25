using InventoryCore.Managers;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace InventoryRESTAPI.Controllers
{
    [ApiController]

    public class InventoryController : ControllerBase
    {
        private readonly IInventoryManager _inventoryManager;

        public InventoryController(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpGet("/")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpPost("UpdateInventory")]
        public async Task<TimeSpan> UpdateInventory([FromBody] UpdateInventoryRequestModel request)
        {
            return await _inventoryManager.UpdateInventory(request);
        }
    }
}
