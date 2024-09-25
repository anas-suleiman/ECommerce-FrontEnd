namespace Shared
{
    public class InventoryItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }

        public bool IsAvailable
        {
            get
            {
                return StockQuantity > 0;
            }
        }
    }
}
