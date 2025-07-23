namespace Application.Interfaces.Validations;

public interface IProductAttributeValueCommandBase
{
    public string Value{get;set;}
    public int ProductId{get;set;}
    public int ProductAttributeId{get;set;}
    public int Stock { get; set; }
    public int ReservedStock { get; set; }

}