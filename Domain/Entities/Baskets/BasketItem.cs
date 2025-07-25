using Domain.Entities.Base;
using Domain.Entities.Products;


namespace Domain.Entities.Baskets
{
    public class BasketItem:BaseEntity
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; } = default!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        public int? ProductAttributeValueId { get; set; }
        public ProductAttributeValue? ProductAttributeValue { get; set; }

        public DateTime? ReservationExpirationDate { get; set; }
    }
}
