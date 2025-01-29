using ElderlyCareSupport.Application.Contracts.Requests;
using FluentValidation;

namespace ElderlyCareSupport.Application.Validators;

public sealed class UserLoginValidator: AbstractValidator<LoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(l => l.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is Invalid");
        
        RuleFor(l => l.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
        
        RuleFor(l => l.UserType).NotEmpty().WithMessage("UserType is required");
    }
}