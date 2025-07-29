using Application.Features.CQRS.Notifications.Commands;
using Application.Features.DTOs.Notifications;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>();

            CreateMap<CreateNotificationCommandRequest, Notification>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());
        }
    }
}
