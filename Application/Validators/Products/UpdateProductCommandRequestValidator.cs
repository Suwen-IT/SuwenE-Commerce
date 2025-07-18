using Application.Features.CQRS.Products.Commands;
using FluentValidation;


namespace Application.Validators.Products
{
    public class UpdateProductCommandRequestValidator:ProductBaseValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Ürün ID'si geçerli bir değer olmalıdır.");
        }
    }
}
