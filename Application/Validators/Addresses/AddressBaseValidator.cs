using Application.Interfaces.Validations;
using FluentValidation;

namespace Application.Validators.Addresses;

public class AddressBaseValidator<T>: AbstractValidator<T>
where T: class, IAddressCommandBase
{
    public AddressBaseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık alanı zorunludur!")
            .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Ülke alanı zorunludur!")
            .MaximumLength(50).WithMessage("Ülke adı en fazla 50 karakter olabilir.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir alanı zorunludur!")
            .MaximumLength(50).WithMessage("Şehir adı en fazla 50 karakter olabilir.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("İlçe alanı zorunludur!")
            .MaximumLength(50).WithMessage("İlçe adı en fazla 50 karakter olabilir.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Adres alanı zorunludur!")
            .MaximumLength(250).WithMessage("Adres alanı en fazla 250 karakter olabilir.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Posta kodu alanı zorunludur!")
            .MaximumLength(10).WithMessage("Posta kodu en fazla 10 karakter olabilir.");


        RuleFor(x => x.AppUserId)
            .NotEmpty().WithMessage("Kullanıcı ID alanı zorunludur! ")
            .NotEqual(Guid.Empty).WithMessage("Kullanıcı ID’si boş bir GUID olamaz.");
    }
    
}