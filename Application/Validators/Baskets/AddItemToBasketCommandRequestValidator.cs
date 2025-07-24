using Application.Features.CQRS.Baskets.Commands;
using FluentValidation;

namespace Application.Validators.Baskets
{
    public class AddItemToBasketCommandRequestValidator:AbstractValidator<AddItemToBasketCommandRequest>
    {
        public AddItemToBasketCommandRequestValidator()
        {
            RuleFor(x => x.AppUserId)
               .NotEmpty().WithMessage("Kullanıcı ID'si boş olamaz.")
               .NotEqual(Guid.Empty).WithMessage("Kullanıcı ID'si geçerli bir değer olmalıdır.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Ürün ID'si geçerli olmalıdır.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.ProductAttributeValueId)
                .GreaterThan(0).When(x => x.ProductAttributeValueId.HasValue)
                .WithMessage("Ürün niteliği değeri ID'si geçerli olmalıdır.");
        }
    }
}
