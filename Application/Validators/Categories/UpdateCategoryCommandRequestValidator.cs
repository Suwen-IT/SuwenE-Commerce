using Application.Features.CQRS.Categories.Commands;
using FluentValidation;

namespace Application.Validators.Categories
{
    public class UpdateCategoryCommandRequestValidator: CategoryBaseValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kategori Id'si boş olamaz.")
                .GreaterThan(0).WithMessage("Geçerli bir kategori ID'si giriniz.");
        }
    }
}
