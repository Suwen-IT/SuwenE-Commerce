namespace Application.Features.DTOs.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }= string.Empty;
        public string ImageUrl { get; set; }= string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public int? ProductAttributeValueId { get; set; }
        public string? ProductAttributeValueName { get; set; } 
        public string? ProductAttributeValue { get; set; } 
        public string? ProductAttributeName { get; set; }

        public DateTime? ReservationExpirationDate { get; set; }

    }
}
