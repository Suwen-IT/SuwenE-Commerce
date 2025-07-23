using Application.Common.Models;
using Application.Features.CQRS.Reviews.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Reviews.Handlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<Review> _readRepository;
        private readonly IWriteRepository<Review> _writeRepository;
        public DeleteReviewCommandHandler(IReadRepository<Review> readRepository, IWriteRepository<Review> writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var review = await _readRepository.GetByIdAsync(request.Id);
            if(review==null)
                return new ResponseModel<NoContent>("Yorum bulunamadı.",404);

            if(review.AppUserId != request.AppUserId)
                return new ResponseModel<NoContent>("Bu yorumu silme yetkiniz yok.", 403);

            await _writeRepository.DeleteAsync(review);
            var result = await _writeRepository.SaveChangesAsync();

            if(!result)
                return new ResponseModel<NoContent>("Yorum silinemedi.", 500);

            return new ResponseModel<NoContent>( new NoContent(),204);
        }
    }
}
