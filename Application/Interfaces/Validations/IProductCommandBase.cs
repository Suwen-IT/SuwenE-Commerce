namespace Application.Interfaces.Validations
{
    public interface IProductCommandBase
    {
        string Name { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
        decimal Price { get; set; }
        int CategoryId { get; set; }
    }
}
