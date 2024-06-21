using FluentValidation;

namespace OutOfOffice.Shared.DTOs.Identity.Validators;

public class UserAuthenticationDtoValidation : AbstractValidator<UserAuthenticationDto>
{
    public UserAuthenticationDtoValidation()
    {
        RuleFor(p => p.UserNameOrEmail)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
    }
}