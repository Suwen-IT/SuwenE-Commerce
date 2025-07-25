using Application.Features.CQRS.Orders.Commands;
using FluentValidation;

namespace Application.Validators.Orders
{
    public class UpdateOrderStatusCommandRequestValidator:AbstractValidator<UpdateOrderStatusCommandRequest>
    {
        public UpdateOrderStatusCommandRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş ID'si geçerli olmalıdır");

            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si boş olamaz");

            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("Geçersiz sipariş durumu.");
        }
    }
}
