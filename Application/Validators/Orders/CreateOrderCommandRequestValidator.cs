using Application.Features.CQRS.Orders.Commands;

namespace Application.Validators.Orders
{
    public class CreateOrderCommandRequestValidator : OrderBaseValidator<CreateOrderCommandRequest>
    {
        public CreateOrderCommandRequestValidator() { }
    }
}
