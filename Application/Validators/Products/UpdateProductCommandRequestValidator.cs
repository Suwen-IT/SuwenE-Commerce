using Application.Features.CQRS.Products.Commands;
using FluentValidation;

namespace Application.Validators.Products
{
    public class UpdateProductCommandRequestValidator : ProductBaseValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandRequestValidator():base()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ürün ID'si giriniz.");
        }
    }
}