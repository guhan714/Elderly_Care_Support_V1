using ElderlyCareSupport.Application.Contracts.Requests;
using FluentValidation;

namespace ElderlyCareSupport.Application.Validators;

public sealed class UserRegistrationValidator : AbstractValidator<RegistrationRequest>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage("First name cannot be empty")
            .MaximumLength(50)
            .MinimumLength(3).WithMessage("First name must be between 3 and 50 characters");
        
        RuleFor(x => x.LastName).NotEmpty()
            .WithMessage("First name cannot be empty")
            .MaximumLength(50)
            .MinimumLength(3).WithMessage("First name must be between 3 and 50 characters");
        
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MaximumLength(100).WithMessage("Maximum length 100 characters.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("Maximum length 100 characters.");
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
        
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(100).WithMessage("Maximum length 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");
        
        RuleFor(x => x.Region)
            .NotEmpty().WithMessage("Region is required.");
        
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.");
        
        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
        
        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("PostalCode is required.");
        
        RuleFor(x => x.UserType)
            .NotEmpty().WithMessage("UserType is required.");
    }
}