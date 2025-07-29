using Application.Common.Models;
using Application.Features.CQRS.Notifications.Queries;
using Application.Features.DTOs.Notifications;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Notifications.Handlers;

public class GetNotificationQueryHandler:IRequestHandler<GetNotificationsQueryRequest, ResponseModel<List<NotificationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetNotificationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ResponseModel<List<NotificationDto>>> Handle(GetNotificationsQueryRequest request, CancellationToken cancellationToken)
    {
        var notifications=await _unitOfWork
            .GetReadRepository<Notification>()
            .GetAllAsync(n=>n.AppUserId == request.UserId);
        
        var notificationsDto = _mapper.Map<List<NotificationDto>>(notifications);
        
        return new ResponseModel<List<NotificationDto>>(notificationsDto,200);
    }
}