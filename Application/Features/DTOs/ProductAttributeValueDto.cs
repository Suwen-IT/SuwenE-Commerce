
namespace Application.Features.DTOs
{
    public class ProductAttributeValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int ProductAttributeId { get; set; }
        public string ProductAttributeName { get; set; } = string.Empty;
    }
}
