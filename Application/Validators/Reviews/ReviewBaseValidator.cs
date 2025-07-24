using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class ReviewBaseValidator<T>:AbstractValidator<T>
    where T : class, IReviewCommandBase
    {
        public ReviewBaseValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün Id alanı boş olmamalıdır.")
                .GreaterThan(0).WithMessage("Ürün ID'si geçerli olmalıdır.");

            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı ID alanı boş olmamalıdır.");

            RuleFor(x => x.Rating)
                .NotEmpty().WithMessage("Puanlama alanı zorunludur.")
                .InclusiveBetween(1, 5).WithMessage("Puanlama 1 ile 5 arasında olmalıdır.");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olmalıdır.");
        }
    }
}
