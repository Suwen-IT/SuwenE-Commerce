using Application.Features.CQRS.Baskets.Commands;
using FluentValidation;

namespace Application.Validators.Baskets
{
    public class UpdateBasketItemCommandRequestValidator: AbstractValidator<UpdateBasketItemCommandRequest>
    {
        public UpdateBasketItemCommandRequestValidator()
        {
            RuleFor(x => x.BasketItemId)
                 .GreaterThan(0).WithMessage("Güncellenecek sepet öğesi ID'si geçerli olmalıdır.");

            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si boş olamaz.")
                .NotEqual(Guid.Empty).WithMessage("Kullanıcı ID'si geçerli bir değer olmalıdır.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
        }
    }
  
}
