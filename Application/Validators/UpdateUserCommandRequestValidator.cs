using Application.Features.CQRS.Users.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateUserCommandRequestValidator:AbstractValidator<UpdateUserCommandRequest>
    {
        public UpdateUserCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kullanıcı ID alanı zorunludur!");

            When(x => x.FirstName != null, () =>
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("İsim alanı zorunludur!")
                    .Length(2, 50).WithMessage("İsim 2-50 karakter arasında olmalıdır.");
            });
            When(x => x.LastName != null, () =>
            {
                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("Soyisim alanı zorunludur!")
                    .Length(2, 50).WithMessage("Soyisim 2-50 karakter arasında olmalıdır.");
            });
            When(x => x.Email != null, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("E-posta alanı zorunludur!")
                    .EmailAddress().WithMessage("Lütfen geçerli bir E-posta adresi giriniz.");
            });
            When(x => x.PhoneNumber != null, () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("Telefon numarası alanı zorunludur!")
                    .Length(11).WithMessage("Telefon numarası 11 karakterden oluşmalıdır!")
                    .Matches("^[0-9]*$").WithMessage("Telefon numarası sadece rakamlardan oluşmalıdır.");
            });
            When(x => x.UserName != null, () =>
            {
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("Kullanıcı adı alanı zorunludur!")
                    .Length(6, 20).WithMessage("Kullanıcı adı 6-20 karakter arasında olmalıdır.")
                    .Matches("^[a-zA-Z0-9]*$").WithMessage("Kullanıcı adı harfler ve rakamlardan oluşmalıdır.");
            });

        }
    }
}
