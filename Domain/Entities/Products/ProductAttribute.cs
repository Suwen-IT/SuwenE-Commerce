using Domain.Entities.Base;

namespace Domain.Entities.Products;

public class ProductAttribute: BaseEntity
{
    public string Name { get; set; }=string.Empty;
    
    public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }=new HashSet<ProductAttributeValue>();
}