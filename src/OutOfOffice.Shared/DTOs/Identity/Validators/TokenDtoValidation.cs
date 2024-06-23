using FluentValidation;

namespace OutOfOffice.Shared.DTOs.Identity.Validators;

public class TokenDtoValidation : AbstractValidator<TokenDto>
{
    public TokenDtoValidation()
    {
        RuleFor(t => t.RefreshToken)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters");

        RuleFor(p => p.AccessToken)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters");
    }
}