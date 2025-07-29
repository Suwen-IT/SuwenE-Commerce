using Application.Features.CQRS.ProductAttributeValues.Commands;
using FluentValidation;

namespace Application.Validators.ProductAttributeValues
{
    public class UpdateProductAttributeValueCommandRequestValidator : ProductAttributeValueBaseValidator<UpdateProductAttributeValueCommandRequest>
    {
        public UpdateProductAttributeValueCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ürün niteliği değeri ID'si giriniz.");
        }
    }
}