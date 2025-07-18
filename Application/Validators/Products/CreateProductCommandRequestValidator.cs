using Application.Features.CQRS.Products.Commands;

namespace Application.Validators.Products
{
    public class CreateProductCommandRequestValidator:ProductBaseValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandRequestValidator()
        {
           
        }
    }
}
