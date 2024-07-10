using FluentValidation;

namespace OutOfOffice.Shared.DTOs.Identity.Validators;

public class TokenDtoValidation : AbstractValidator<TokenDto>
{
    public TokenDtoValidation()
    {
        RuleFor(t => t.RefreshToken)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(5000).WithMessage("{PropertyName} must not exceed 5000 characters");

        RuleFor(p => p.AccessToken)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(5000).WithMessage("{PropertyName} must not exceed 5000 characters");
    }
}