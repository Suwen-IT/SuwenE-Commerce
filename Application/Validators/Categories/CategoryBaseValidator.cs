

using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.Categories
{
    public abstract class CategoryBaseValidator<T>:AbstractValidator<T> where T : class, ICategoryCommandBase
    {
        protected CategoryBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı girmek zorunludurç")
                .Length(3,30).WithMessage("Kategori adı 5-30 karakter arasında olmalıdır.");
        }
    }
}
