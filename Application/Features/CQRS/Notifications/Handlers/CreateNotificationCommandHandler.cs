using Application.Common.Models;
using Application.Features.CQRS.Notifications.Commands;
using Application.Features.DTOs.Notifications;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Notifications.Handlers;

public class CreateNotificationCommandHandler:IRequestHandler<CreateNotificationCommandRequest, ResponseModel<NotificationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseModel<NotificationDto>> Handle(CreateNotificationCommandRequest request, CancellationToken cancellationToken)
    {
        var notification = _mapper.Map<Notification>(request);

        await _unitOfWork.GetWriteRepository<Notification>().AddAsync(notification);
        var success = await _unitOfWork.SaveChangesBoolAsync();

        if (!success)
            return new ResponseModel<NotificationDto>("Bildirim oluşturulamadı.", 500);
        
        var notificationDto = _mapper.Map<NotificationDto>(notification);
        return new ResponseModel<NotificationDto>(notificationDto,201);
    }
}