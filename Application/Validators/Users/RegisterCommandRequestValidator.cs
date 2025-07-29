using Application.Features.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validators.Users;

public class RegisterCommandRequestValidator:AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alanı zorunludur!")
            .EmailAddress().WithMessage("Lütfen geçerli bir E-posta adresi giriniz. ");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı zorunludur!")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakterden oluşmalıdır!")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf i�ermelidir!")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf i�ermelidir!")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir!")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir!");
        
        RuleFor(x=>x.ConfirmPassword)
            .NotEmpty().WithMessage("Şifre onaylama alanı zorunludur!")
            .Equal(x => x.Password).WithMessage("Girrdiğiniz şifreler uyuşmamaktadır.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("İsim alanı zorunludur!");
        
        RuleFor(x=>x.LastName)
            .NotEmpty().WithMessage("Soyisim alanı zorunludur!");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı alanı zorunludur!")
            .Length(6, 20).WithMessage("Kullanıcı adı 6-20 karakter arasında olmalıdır.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Kulllanıcı adı harfler ve rakamlardan oluşmalıdır.");
        
        RuleFor(x=>x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası alanı zorunludur!")
            .Length(11).WithMessage("Telefon numarası 11 karakerden oluşmalıdır!")
            .Matches("^[0-9]*$").WithMessage("Telefon numarası sadece rakamlardan oluşmalıdır.");

    }
}