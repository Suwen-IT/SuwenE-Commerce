using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
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