using Application.Features.CQRS.Reviews.Commands;
using Application.Features.DTOs.Reviews;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.UserName));

            CreateMap<CreateReviewCommandRequest, Review>()
                .ForMember(dest=>dest.Id, opt => opt.Ignore())
                .ForMember(dest=>dest.CreatedDate,opt=>opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<UpdateReviewCommandRequest, Review>()
                .ForMember(dest=> dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewDate, opt => opt.Ignore());

        }
    }
}
