namespace Shared.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string EmailAddress { get; set; }
        public PaymentItem Payment { get; set; }
    }
}
