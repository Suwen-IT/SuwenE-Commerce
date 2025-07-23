using Application.Features.CQRS.Addresses.Commands;

namespace Application.Validators.Addresses;

public class CreateAddressCommandRequestValidator:
    AddressBaseValidator<CreateAddressCommandRequest>
{
   public CreateAddressCommandRequestValidator():base()
   {
       
   }
}