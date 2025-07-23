using Application.Features.CQRS.Reviews.Commands;

namespace Application.Validators.Reviews
{
    public class CreateReviewCommandRequestValidator : ReviewBaseValidator<CreateReviewCommandRequest>
    {
        public CreateReviewCommandRequestValidator()
        {

        }
    }
}
