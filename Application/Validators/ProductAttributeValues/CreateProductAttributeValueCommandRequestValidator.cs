using Application.Features.CQRS.ProductAttributeValues.Commands;

namespace Application.Validators.ProductAttributeValues
{
    public class CreateProductAttributeValueCommandRequestValidator : ProductAttributeValueBaseValidator<CreateProductAttributeValueCommandRequest>
    {
        public CreateProductAttributeValueCommandRequestValidator()
        {
        }
    }
}