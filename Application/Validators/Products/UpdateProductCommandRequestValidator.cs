using Application.Features.CQRS.Products.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators.Products
{
    public class UpdateProductCommandRequestValidator : ProductBaseValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandRequestValidator():base()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ürün ID'si giriniz.");
        }
    }
}