using Application.Features.CQRS.Addresses.Commands;
using FluentValidation;

namespace Application.Validators.Addresses;

public class UpdateAddressCommandRequestValidator:AddressBaseValidator<UpdateAddressCommandRequest>
{
    public UpdateAddressCommandRequestValidator()
    {
        RuleFor(x=>x.Id)
            .NotEmpty().WithMessage("Adres ID alanı zorunludur!")
            .GreaterThan(0).WithMessage("Geçerli bir adres id giriniz.");
    }
    
}