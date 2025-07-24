using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.ProductAttributes
{
    public class ProductAttributeBaseValidator<T>:AbstractValidator<T> where T :class, IProductAttributeCommandBase
    {
        public ProductAttributeBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün niteliği alanı zorunludur.")
                .Length(2, 50).WithMessage("Ürün niteliği adı alanı 2-50 karakter arasında olmalıdır.");
        }
    }
}
