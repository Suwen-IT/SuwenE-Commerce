using Application.Features.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validators.Users;

public class RegisterCommandRequestValidator:AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alaný zorunludur!")
            .EmailAddress().WithMessage("Lütfen geçerli bir E-posta adresi giriniz. ");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Þifre alaný zorunludur!")
            .MinimumLength(6).WithMessage("Þifre en az6 karakterden oluþmalýdýr!")
            .Matches("[A-Z]").WithMessage("Þifre en az bir büyük harf içermelidir!")
            .Matches("[a-z]").WithMessage("Þifre en az bir küçük harf içermelidir!")
            .Matches("[0-9]").WithMessage("Þifre en az bir rakam içermelidir!")
            .Matches("[^a-zA-Z0-9]").WithMessage("Þifre en az bir özel karakter içermelidir!");
        
        RuleFor(x=>x.ConfirmPassword)
            .NotEmpty().WithMessage("Þifre onaylama alaný zorunludur!")
            .Equal(x => x.Password).WithMessage("Girrdiðiniz þifreler uyuþmamaktadýr.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ýsim alaný zorunludur!");
        
        RuleFor(x=>x.LastName)
            .NotEmpty().WithMessage("Soyisim alaný zorunludur!");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanýcý adý alaný zorunludur!")
            .Length(6, 20).WithMessage("Kullanýcý adý 6-20 karakter arasýnda olmalýdýr.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Kulllanýcý adý harfler ve rakamlardan oluþmalýdýr.");
        
        RuleFor(x=>x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarasý alaný zorunludur!")
            .Length(11).WithMessage("Telefon numarasý 11 karakerden oluþmalýdýr!")
            .Matches("^[0-9]*$").WithMessage("Telefon numarasý sadece rakamlardan oluþmalýdýr.");

    }
}