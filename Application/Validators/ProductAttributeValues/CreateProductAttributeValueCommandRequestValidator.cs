using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators.ProductAttributeValues
{
    public class CreateProductAttributeValueCommandRequestValidator : ProductAttributeValueBaseValidator<CreateProductAttributeValueCommandRequest>
    {
        public CreateProductAttributeValueCommandRequestValidator()
        {
        }
    }
}