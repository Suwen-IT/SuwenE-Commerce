using Domain.Entities.Base;
using Domain.Entities.Baskets;
using Domain.Entities.Orders;

namespace Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        public  string Name { get; set; }=string.Empty;
        public  string Description { get; set; }=string.Empty;
        public  string ImageUrl { get; set; }=string.Empty;
        public  decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new HashSet<ProductAttributeValue>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<BasketItem>BasketItems { get; set; } = new HashSet<BasketItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }
}
