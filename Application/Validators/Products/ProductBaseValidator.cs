using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.Products
{
    public abstract class ProductBaseValidator<T> : AbstractValidator<T>
        where T : class, IProductCommandBase
    { 
        protected ProductBaseValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı zorunludur.")
                .Length(5, 100).WithMessage("Ürün adı 5-100 karakter arasında olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Ürün açıklaması zorunludur.")
                .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olmalıdır.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Ürün görsel URL'si giriniz.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Ürün görsel URL'si geçerli bir URL olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori ID'si giriniz.");
        }
    }
}