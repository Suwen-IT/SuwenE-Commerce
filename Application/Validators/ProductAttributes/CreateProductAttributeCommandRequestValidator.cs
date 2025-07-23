using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.CQRS.ProductAttributeValues.Commands;


namespace Application.Validators.ProductAttributes
{
    public class CreateProductAttributeCommandRequestValidator:ProductAttributeBaseValidator<CreateProductAttributeCommandRequest>
    {
        public CreateProductAttributeCommandRequestValidator()
        {
            
        }
    }
    
}
