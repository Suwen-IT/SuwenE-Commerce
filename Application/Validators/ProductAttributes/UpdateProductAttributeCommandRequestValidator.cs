using Application.Features.CQRS.ProductAttributes.Commands;
using FluentValidation;

namespace Application.Validators.ProductAttributes
{
    public class UpdateProductAttributeCommandRequestValidator 
        : ProductAttributeBaseValidator<UpdateProductAttributeCommandRequest>
    {
        public UpdateProductAttributeCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Ürün niteliği ID'si geçerli olmalıdır.");
        }
    }
}