using Domain.Entities.Base;

namespace Domain.Entities;

public class ProductAttributeValue: BaseEntity
{
    public string Value { get; set; }=string.Empty;
    
    public int ProductAttributeId { get; set; }
    public ProductAttribute ProductAttribute { get; set; } = default!;
    
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public int Stock { get; set; }
    public int ReservedStock { get; set; } 

}