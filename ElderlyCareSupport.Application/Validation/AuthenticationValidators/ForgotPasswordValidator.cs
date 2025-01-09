using ElderlyCareSupport.Application.DTOs;
using FluentValidation;

namespace ElderlyCareSupport.Application.Validation.AuthenticationValidators;

public sealed class ForgotPasswordValidator: AbstractValidator<string>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x).NotEmpty()
            .EmailAddress().WithMessage("Please enter a valid email address.");
    }
}