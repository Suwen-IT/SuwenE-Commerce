using Application.Features.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validators;

public class RegisterCommandRequestValidator:AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Please enter a valid email address!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required!")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters!")
            .Matches("[A-Z]").WithMessage("Password must contain at least one upper case letter!")
            .Matches("[a-z]").WithMessage("Password must contain at least one lower case!")
            .Matches("[0-9]").WithMessage("Password must contain at least one numbers!")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        
        RuleFor(x=>x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required!")
            .Equal(x => x.Password).WithMessage("Confirm password does not match!");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required!");
        
        RuleFor(x=>x.LastName)
            .NotEmpty().WithMessage("Last name is required!");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required!")
            .Length(6, 20).WithMessage("Username must be between 6 and 20 characters!")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Username must contain only letters and numbers.");
        
        RuleFor(x=>x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required!")
            .Length(11).WithMessage("Phone number must be 11 digits!")
            .Matches("^[0-9]*$").WithMessage("Phone number must contain only numbers.");

    }
}