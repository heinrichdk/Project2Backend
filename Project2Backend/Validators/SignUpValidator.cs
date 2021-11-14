using FluentValidation;
using Project2Backend.Models.Requests;

namespace Project2Backend.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(64).WithMessage("Name length must not exceed 64 characters");
        RuleFor(x => x.Surname).MaximumLength(64).WithMessage("Surname length must not exceed 64 characters");
        RuleFor(x => x.Username).NotEmpty().WithMessage("Please specify a username");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Please specify a password")
            .MinimumLength(8).WithMessage("Password length must be at least 8 characters")
            .MaximumLength(64).WithMessage("Password length must not exceed 64 characters")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number");
    }
}