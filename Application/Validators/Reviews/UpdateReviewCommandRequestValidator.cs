using Application.Features.CQRS.Reviews.Commands;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class UpdateReviewCommandRequestValidator : ReviewBaseValidator<UpdateReviewCommandRequest>
    {
        public UpdateReviewCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Yorum ID alanı boş olmamalıdır.")
                .GreaterThan(0).WithMessage("Yorum ID'si geçerli olmalıdır.");
        }
    }
}
