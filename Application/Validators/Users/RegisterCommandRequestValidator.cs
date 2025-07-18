using Application.Features.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validators.Users;

public class RegisterCommandRequestValidator:AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alan� zorunludur!")
            .EmailAddress().WithMessage("L�tfen ge�erli bir E-posta adresi giriniz. ");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("�ifre alan� zorunludur!")
            .MinimumLength(6).WithMessage("�ifre en az6 karakterden olu�mal�d�r!")
            .Matches("[A-Z]").WithMessage("�ifre en az bir b�y�k harf i�ermelidir!")
            .Matches("[a-z]").WithMessage("�ifre en az bir k���k harf i�ermelidir!")
            .Matches("[0-9]").WithMessage("�ifre en az bir rakam i�ermelidir!")
            .Matches("[^a-zA-Z0-9]").WithMessage("�ifre en az bir �zel karakter i�ermelidir!");
        
        RuleFor(x=>x.ConfirmPassword)
            .NotEmpty().WithMessage("�ifre onaylama alan� zorunludur!")
            .Equal(x => x.Password).WithMessage("Girrdi�iniz �ifreler uyu�mamaktad�r.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("�sim alan� zorunludur!");
        
        RuleFor(x=>x.LastName)
            .NotEmpty().WithMessage("Soyisim alan� zorunludur!");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullan�c� ad� alan� zorunludur!")
            .Length(6, 20).WithMessage("Kullan�c� ad� 6-20 karakter aras�nda olmal�d�r.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Kulllan�c� ad� harfler ve rakamlardan olu�mal�d�r.");
        
        RuleFor(x=>x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numaras� alan� zorunludur!")
            .Length(11).WithMessage("Telefon numaras� 11 karakerden olu�mal�d�r!")
            .Matches("^[0-9]*$").WithMessage("Telefon numaras� sadece rakamlardan olu�mal�d�r.");

    }
}