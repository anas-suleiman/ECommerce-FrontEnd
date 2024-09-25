using Grpc.Core;
using Shared;
using System.Diagnostics;

namespace InventorygRPC.Services
{
    public class InventoryService : Inventory.InventoryBase
    {
        private readonly ILogger<InventoryService> _logger;

        private static List<InventoryItem> _inventoryItems = new List<InventoryItem> {
            new InventoryItem{ ProductId = 1, ProductName = "EcoScent Candles",  StockQuantity = 50000},
            new InventoryItem{ ProductId = 2, ProductName = "TechTote Bags", StockQuantity = 50000},
            new InventoryItem{ ProductId = 3, ProductName = "Urban Blend Spices", StockQuantity = 50000},
            new InventoryItem{ ProductId = 4, ProductName = "FitTrack Smartwatch", StockQuantity = 50000},
            new InventoryItem{ ProductId = 5, ProductName = "ComfyNest Pet Bed", StockQuantity = 50000},
            new InventoryItem{ ProductId = 6, ProductName = "Artisan Crafted Jewelry",  StockQuantity = 50000},
            new InventoryItem{ ProductId = 7, ProductName = "Gourmet Coffee Subscription", StockQuantity = 50000},
            new InventoryItem{ ProductId = 8, ProductName = "ZenGarden Indoor Plants",  StockQuantity = 50000},
            new InventoryItem{ ProductId = 9, ProductName = "Stellar Travel Backpack", StockQuantity = 50000},
            new InventoryItem{ ProductId = 10, ProductName = "Creative Art Kits",  StockQuantity = 50000}
            };
        public InventoryService(ILogger<InventoryService> logger)
        {
            _logger = logger;
        }

        public override async Task<InventoryResponse> UpdateInventory(UpdateInventoryRequestModel request, ServerCallContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate processing of the inventory update
            await Task.Delay(100);

            stopwatch.Stop();

            return new InventoryResponse
            {
                ProcessingTime = stopwatch.ElapsedMilliseconds.ToString()
            };
            
        }
    }
}
