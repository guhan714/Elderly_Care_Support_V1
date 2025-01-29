using FluentValidation;

namespace ElderlyCareSupport.Application.Validators;

public sealed class ForgotPasswordValidator: AbstractValidator<string>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x).NotEmpty()
            .EmailAddress().WithMessage("Please enter a valid email address.");
    }
}