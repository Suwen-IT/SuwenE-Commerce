using Application.Common.Models;
using Application.Features.CQRS.Notifications.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.Notifications.Handlers;

public class MarkAsReadCommandHandler:IRequestHandler<MarkAsReadCommandRequest ,ResponseModel<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkAsReadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
        
    public async Task<ResponseModel<NoContent>> Handle(MarkAsReadCommandRequest request, CancellationToken cancellationToken)
    {
        var notification=await _unitOfWork.GetReadRepository<Notification>().GetByIdAsync(request.NotificationId);

        if (notification == null)
            return new ResponseModel<NoContent>("Bildirim bulunamadı.",404);
        
        notification.IsRead = true;
        await _unitOfWork.GetWriteRepository<Notification>().UpdateAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        
        return new ResponseModel<NoContent>(new NoContent(),200);
    }
}