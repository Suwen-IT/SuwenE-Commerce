using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators
{
    public abstract class ProductBaseValidator<T>:AbstractValidator<T>
    where T : class, IProductCommandBase
    {
        public ProductBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı zorunludur.")
                .Length(5, 100).WithMessage("Ürün adı 5-100 karakter arasında olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Ürün açıklama adı zorunludur.")
                .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olmalıdır.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Ürün görsel URL'si giriniz.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Ürün görsel URL'si geçerli bir URL olmalıdır. ");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı sıfırdan küçük olmamalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategory ID'si giriniz.");
        }
    }
}
