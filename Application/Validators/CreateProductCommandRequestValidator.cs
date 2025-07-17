using Application.Features.CQRS.Products.Commands;

namespace Application.Validators
{
    public class CreateProductCommandRequestValidator:ProductBaseValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandRequestValidator()
        {
           
        }
    }
}
