using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.Orders
{
    public class OrderBaseValidator<T>:AbstractValidator<T> where T : class, IOrderBaseCommand
    {
        public OrderBaseValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si boş olamaz.");

            RuleFor(x => x.ShippingAddressId)
                .GreaterThan(0).WithMessage("Geçerli bir teslimat adresi ID'si giriniz.");

            RuleFor(x => x.BillingAddressId)
                .GreaterThan(0).When(x => x.BillingAddressId.HasValue)
                .WithMessage("Geçerli bir fatura adresi ID'si giriniz.");
        }
    }
}
