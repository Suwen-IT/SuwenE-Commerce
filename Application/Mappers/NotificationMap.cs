using Application.Features.DTOs.Notifications;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class NotificationMap:Profile
    {
        public NotificationMap()
        {
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationCreateDto, Notification>();
        }
    }
}
