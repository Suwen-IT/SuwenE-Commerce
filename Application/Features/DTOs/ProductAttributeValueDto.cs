
namespace Application.Features.DTOs
{
    public class ProductAttributeValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int ProductAttributeId { get; set; }
        public int ProductId { get; set; }

        public string? ProductAttributeName { get; set; }
        public string? ProductName { get; set; }
        public int Stock { get; set; }
        public int ReservedStock { get; set; }

    }
}
