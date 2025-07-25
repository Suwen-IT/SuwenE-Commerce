namespace Application.Features.DTOs.Orders
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }=string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public int? ProductAttributeValueId { get; set; }
        public string? ProductAttributeValueName { get; set; } = string.Empty; 
        public string? ProductAttributeName { get; set; } = string.Empty;
    }
}
