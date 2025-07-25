using Application.Features.CQRS.Orders.Commands;
using FluentValidation;

namespace Application.Validators.Orders
{
    public class UpdateOrderCommandRequestValidator:OrderBaseValidator<UpdateOrderCommandRequest>
    {
        public UpdateOrderCommandRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş ID'si geçerli olmalıdır.");
        }
    }

}
