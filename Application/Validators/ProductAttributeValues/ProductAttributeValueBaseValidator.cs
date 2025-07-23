using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.ProductAttributeValues;

public class ProductAttributeValueBaseValidator<T> : AbstractValidator<T>
    where T : class, IProductAttributeValueCommandBase
{
    public ProductAttributeValueBaseValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Geçerli bir ürün ID giriniz.");

        RuleFor(x => x.ProductAttributeId)
            .GreaterThan(0).WithMessage("Geçerli bir ürün özelliği ID giriniz.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Değer alanı boş olamaz.")
            .MaximumLength(255).WithMessage("Değer 255 karakteri geçemez.");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage("Stok alanı boş olamaz.")
            .GreaterThanOrEqualTo(0).WithMessage("Stok değeri negatif olamaz.");

        RuleFor(x => x.ReservedStock)
            .GreaterThanOrEqualTo(0).WithMessage("Rezerve stok 0 veya daha büyük olmalıdır.");

    }
}