using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, ResponseModel<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ResponseModel<NoContentDto>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return new ResponseModel<NoContentDto>("Kulanıcı Bullunamadı", 404);
            }
            _mapper.Map(request, user);

           if(request.Email != null &&!string.Equals(user.Email,request.Email,StringComparison.OrdinalIgnoreCase))
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, request.Email);
                if (!setEmailResult.Succeeded)
                {
                    return new ResponseModel<NoContentDto>(setEmailResult.Errors.FirstOrDefault()?.Description??"E-posta adresi güncellenmedi.", 400);
                }
                user.EmailConfirmed = false;
            }
           if(request.PhoneNumber != null && !string.Equals(user.PhoneNumber, request.PhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    return new ResponseModel<NoContentDto>(setPhoneResult.Errors.FirstOrDefault()?.Description ?? "Telefon numarası güncellenmedi.", 400);
                }
                user.PhoneNumberConfirmed = false;
            }
           if(request.UserName != null && !string.Equals(user.UserName, request.UserName, StringComparison.OrdinalIgnoreCase))
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, request.UserName);
                if (!setUserNameResult.Succeeded)
                {
                    return new ResponseModel<NoContentDto>(setUserNameResult.Errors.FirstOrDefault()?.Description ?? "Kullanıcı adı güncellenmedi.", 400);
                }
            }
            user.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                return new ResponseModel<NoContentDto>(new NoContentDto(), 204);
            }
            return new ResponseModel<NoContentDto>(updateResult.Errors.FirstOrDefault()?.Description ?? "Kullanıcı bilgileri güncellenmedi.", 400);
        }
    }
}
