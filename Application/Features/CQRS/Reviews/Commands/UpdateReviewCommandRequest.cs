using Application.Common.Models;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.Reviews.Commands
{
    public class UpdateReviewCommandRequest:IRequest<ResponseModel<ReviewDto>>, IReviewCommandBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Guid AppUserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
